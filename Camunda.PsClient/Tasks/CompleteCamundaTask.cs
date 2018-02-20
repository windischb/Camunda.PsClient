using System.Collections;
using System.Management.Automation;
using doob.Camunda.Client;
using doob.Camunda.Client.Tasks;

namespace Camunda.PsClient.Tasks
{

    [Cmdlet(VerbsLifecycle.Complete, "CamundaTask")]
    public class CompleteCamundaTask : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true)]
        public string Id { get; set; }

        [Parameter(Mandatory = false)]
        public Hashtable Variables { get; set; }

        [Parameter(Mandatory = false)]
        public CamundaClient Client { get; set; }


        protected override void ProcessRecord()
        {
            var tasks = GlobalHelpers.GetCamundaClient(Client).Task;
            var result = tasks.Complete(Id, Options).GetAwaiter().GetResult();
        }

        private void Options(CompleteOptionsBuilder completeOptions) {

            if (Variables != null)
            {
                foreach (var key in Variables.Keys)
                {
                    completeOptions.AddVariable(key.ToString(), Variables[key.ToString()]);
                }
            }
        }

    }
}
