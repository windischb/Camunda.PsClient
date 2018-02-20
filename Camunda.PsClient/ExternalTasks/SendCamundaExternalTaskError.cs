using System;
using System.Management.Automation;
using doob.Camunda.Client;
using doob.Camunda.Client.ExternalTasks;

namespace Camunda.PsClient.ExternalTasks
{

    [Cmdlet(VerbsCommunications.Send, "CamundaExternalTaskError")]
    public class SendCamundaExternalTaskError : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true)]
        public string Id { get; set; }

        [Parameter(Mandatory = true, Position = 1, ValueFromPipelineByPropertyName = true)]
        public string WorkerId { get; set; }

        [Parameter(Mandatory = false)]
        public string ErrorCode { get; set; }

        [Parameter(Mandatory = false)]
        public CamundaClient Client { get; set; }


        protected override void ProcessRecord()
        {
            var exTasks = GlobalHelpers.GetCamundaClient(Client).ExternalTask;
            var result = exTasks.HandleBpmnError(Id, Builder).GetAwaiter().GetResult();
            
        }

        private void Builder(HandleBpmnErrorParametersBuilder builder) {
            builder.WithErrorCode(ErrorCode);
            builder.WithWorkerId(WorkerId);
        }
    }
}
