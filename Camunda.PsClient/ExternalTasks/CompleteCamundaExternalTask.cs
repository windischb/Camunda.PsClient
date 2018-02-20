using System.Collections;
using System.Management.Automation;
using doob.Camunda.Client;
using doob.Camunda.Client.ExternalTasks;

namespace Camunda.PsClient.ExternalTasks
{

    [Cmdlet(VerbsLifecycle.Complete, "CamundaExternalTask")]
    public class CompleteCamundaExternalTask : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true)]
        public string Id { get; set; }

        [Parameter(Mandatory = false, Position = 1, ValueFromPipelineByPropertyName = true)]
        public string WorkerId { get; set; }

        [Parameter(Mandatory = false)]
        public Hashtable Variables { get; set; }

        [Parameter(Mandatory = false)]
        public CamundaClient Client { get; set; }


        protected override void ProcessRecord()
        {
            var exTasks = GlobalHelpers.GetCamundaClient(Client).ExternalTask;
            var result = exTasks.Complete(Id, Options).GetAwaiter().GetResult();
        }

        private void Options(CompleteParametersBuilder completeOptions) {
            completeOptions.WithWorkerId(WorkerId);

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
