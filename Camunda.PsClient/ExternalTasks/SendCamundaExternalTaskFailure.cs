using System;
using System.Management.Automation;
using doob.Camunda.Client;
using doob.Camunda.Client.ExternalTasks;

namespace Camunda.PsClient.ExternalTasks
{

    [Cmdlet(VerbsCommunications.Send, "CamundaExternalTaskFailure")]
    public class SendCamundaExternalTaskFailure : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true)]
        public string Id { get; set; }

        [Parameter(Mandatory = true, Position = 1, ValueFromPipelineByPropertyName = true)]
        public string WorkerId { get; set; }

        [Parameter(Mandatory = false)]
        public string ErrorMessage { get; set; }

        [Parameter(Mandatory = false)]
        public string ErrorDetails { get; set; }

        [Parameter(Mandatory = false)]
        public int Retries { get; set; } = 1;

        [Parameter(Mandatory = false)]
        public TimeSpan RetryTimeout { get; set; } = TimeSpan.FromSeconds(10);

        [Parameter(Mandatory = false)]
        public CamundaClient Client { get; set; }


        protected override void ProcessRecord()
        {
            var exTasks = GlobalHelpers.GetCamundaClient(Client).ExternalTask;

            var result = exTasks.HandleFailure(Id, Builder).GetAwaiter().GetResult();
            
        }

        private void Builder(HandleFailureParametersBuilder builder) {
            builder.WithWorkerId(WorkerId);
            builder.WithErrorDetails(ErrorDetails);
            builder.WithErrorMessage(ErrorMessage);
            builder.WithRetries(Retries);
            builder.WithRetryTimeout(RetryTimeout);
        }
    }
}
