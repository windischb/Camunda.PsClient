using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using System.Net.Http;
using System.Threading.Tasks;
using doob.Camunda.Client;
using doob.Camunda.Client.Global;
using doob.Camunda.Client.ProcessDefinition;

namespace Camunda.PsClient.ProcessDefinitions
{
    [Cmdlet(VerbsCommon.Get, "CamundaStartFormVariables", DefaultParameterSetName = "ById")]
    public class GetCamundaStartFormVariables : PSCmdlet
    {
       
        [Parameter(Mandatory = true, ParameterSetName = "ById", Position = 0)]
        public string Id { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "ByKey", Position = 0)]
        public string Key { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = "ByKey", Position = 1)]
        public string TenantId { get; set; }

        [Parameter(Mandatory = false)]
        public string[] VariableName { get; set; }

        [Parameter(Mandatory = false)]
        public CamundaClient Client { get; set; }

        protected override void ProcessRecord() {


            Task<Dictionary<string, VariableValue>> req = null;

            if (this.ParameterSetName == "ById") {
                 req = GlobalHelpers.GetCamundaClient(Client).ProcessDefinition.GetStartFormVariablesById(Id, VariableName);
            } else {
                req = GlobalHelpers.GetCamundaClient(Client).ProcessDefinition.GetStartFormVariablesByKey(Key, TenantId, VariableName);
            }

            var result = req.GetAwaiter().GetResult();

            WriteObject(result);

        }

        
    }
}
