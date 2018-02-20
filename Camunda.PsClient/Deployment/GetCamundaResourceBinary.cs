using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using doob.Camunda.Client;
using doob.Camunda.Client.Deployment;

namespace Camunda.PsClient.Deployment {

    [Cmdlet(VerbsCommon.Get, "CamundaResourceBinary")]
    public class GetCamundaResourceBinary : PSCmdlet {

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public ResourceObject[] ResourceObject { get; set; }
        
        [Parameter(Mandatory = true)]
        public string OutputPath { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter OverwriteIfExists { get; set; }

        [Parameter(Mandatory = false)]
        public CamundaClient Client { get; set; }

        protected override void ProcessRecord() {

            var fileMode = OverwriteIfExists ? FileMode.Create : FileMode.CreateNew;

            foreach (var resource in ResourceObject) {
                var stream = GetResourceBinary(resource.DeploymentId, resource.Id);

                var path = Path.Combine(OutputPath, resource.Name);

                var fs = new FileStream(path, fileMode, FileAccess.Write);
                stream.CopyTo(fs);

                fs.Flush(true);
                fs.Close();
                fs.Dispose();
                fs = null;
                
                stream.Close();
                stream.Dispose();
                stream = null;
            }

        }

       
        private Stream GetResourceBinary(string deploymentId, string resourceId) {

            var req = GlobalHelpers.GetCamundaClient(Client)
                .Deployment.GetResourceBinary(deploymentId, resourceId);

            return req.GetAwaiter().GetResult();

        }
    }
}
