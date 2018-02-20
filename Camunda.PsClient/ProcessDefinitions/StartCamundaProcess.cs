using System.Collections;
using System.Management.Automation;
using System.Threading.Tasks;
using doob.Camunda.Client;
using doob.Camunda.Client.ProcessDefinition;

namespace Camunda.PsClient.ProcessDefinitions
{
    [Cmdlet(VerbsLifecycle.Start, "CamundaProcess", DefaultParameterSetName = "ById")]
    public class StartCamundaProcess : PSCmdlet
    {
       
        [Parameter(Mandatory = true, ParameterSetName = "ById", Position = 0)]
        public string Id { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "ByKey", Position = 0)]
        public string Key { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = "ByKey", Position = 1)]
        public string TenantId { get; set; }

        [Parameter(Mandatory = false)]
        public Hashtable Variables { get; set; }

        [Parameter(Mandatory = false)]
        public string BusinessKey { get; set; }

        [Parameter(Mandatory = false)]
        public string CaseInstanceId { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter SkipCustomListeners { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter SkipIoMappings { get; set; }

        [Parameter(Mandatory = false)]
        public bool ReturnVariables { get; set; }


        [Parameter(Mandatory = false)]
        public CamundaClient Client { get; set; }

        protected override void ProcessRecord() {


            Task<StartInstanceObject> req = null;

            if (this.ParameterSetName == "ById") {
                 req = GlobalHelpers.GetCamundaClient(Client).ProcessDefinition.StartInstanceById(Id, Options);
            } else {
                req = GlobalHelpers.GetCamundaClient(Client).ProcessDefinition.StartInstanceByKey(Key, TenantId, Options);
            }

            var result = req.GetAwaiter().GetResult();

            WriteObject(result);

        }

        private void Options(StartInstancePropertiesBuilder startInstanceOptions) {

            if (Variables != null) {
                foreach (var key in Variables.Keys)
                {
                    startInstanceOptions.AddVariable(key.ToString(), Variables[key.ToString()]);
                }
            }

            startInstanceOptions.WithBusinessKey(BusinessKey);
            startInstanceOptions.WithCaseInstanceId(CaseInstanceId);
            startInstanceOptions.SkipCustomListeners(SkipCustomListeners);
            startInstanceOptions.SkipIoMappings(SkipIoMappings);
            startInstanceOptions.WithVariablesInReturn(ReturnVariables);

        }

       
    }
}
