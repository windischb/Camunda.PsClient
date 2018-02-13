using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using doob.Camunda.Client;
using doob.Camunda.Client.Deployment;

namespace Camunda.PsClient.Deployment {

    [Cmdlet(VerbsCommon.New, "CamundaDeployment")]
    public class NewCamundaDeployment : PSCmdlet {
        [Parameter(Mandatory = true)]
        public string Name { get; set; }

        [Parameter(Mandatory = false)]
        public string[] Resources { get; set; } = new string[0];

        [Parameter(Mandatory = false)]
        public string Source { get; set; }

        [Parameter(Mandatory = false)]
        public string TenantId { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter EnableDuplicateFiltering { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter DeployChangedOnly { get; set; }

        [Parameter(Mandatory = false)]
        public CamundaClient Client { get; set; }


        protected override void ProcessRecord() {

            List<FileInfo> resources = new List<FileInfo>();
            bool error = false;
            foreach (var resource in Resources) {
                var fileInfo = new FileInfo(resource);
                if (!fileInfo.Exists) {
                    WriteError(new ErrorRecord(new FileNotFoundException("File not found!", fileInfo.FullName), "0", ErrorCategory.ObjectNotFound, fileInfo));
                    error = true;
                } else {
                    resources.Add(fileInfo);
                }

            }

            if (error)
                return;

            var req = GlobalHelpers.GetCamundaClient(Client)
                .Deployment
                .Create(Name, builder => {
                    builder.EnableDuplicateFiltering(EnableDuplicateFiltering)
                        .DeployChangedOnly(DeployChangedOnly)
                        .WithDeploymentSource(Source)
                        .WithTenantId(TenantId);

                    resources.ForEach(r => {
                        builder.AddResource(r.Name, r.FullName);
                    });
                });


            var result = req.GetAwaiter().GetResult();

            WriteObject(result);
        }
    }
}
