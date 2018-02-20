using System;
using System.Collections.Generic;
using System.Management.Automation;
using doob.Camunda.Client;
using doob.Camunda.Client.ExternalTasks;
using doob.Camunda.Client.Global;
using doob.Camunda.Client.VariableInstances;
using SortBy = doob.Camunda.Client.ExternalTasks.SortBy;

namespace Camunda.PsClient.ExternalTasks
{

    [Cmdlet(VerbsCommon.Get, "CamundaExternalTask")]
    public class GetCamundaExternalTask : PSCmdlet
    {
        [Parameter(Mandatory = false)]
        public string Id { get; set; }

        [Parameter(Mandatory = false)]
        public string TopicName { get; set; }

        [Parameter(Mandatory = false)]
        public string WorkerId { get; set; }

        [Parameter(Mandatory = false)]
        public string ActivityId { get; set; }

        [Parameter(Mandatory = false)]
        public string[] ActivityIdIn { get; set; }

        [Parameter(Mandatory = false)]
        public string ExecutionId { get; set; }

        [Parameter(Mandatory = false)]
        public string ProcessInstanceId { get; set; }

        [Parameter(Mandatory = false)]
        public string ProcessDefinitionId { get; set; }

        [Parameter(Mandatory = false)]
        public string[] TenantIdIn { get; set; }

        [Parameter(Mandatory = false)]
        public int? PriorityHigherThanOrEquals { get; set; }

        [Parameter(Mandatory = false)]
        public int? PriorityLowerThanOrEquals { get; set; }


        [Parameter(Mandatory = false)]
        public SwitchParameter OnlyActive { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter OnlySuspended { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter Locked { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter WithRetriesLeft { get; set; }

        [Parameter(Mandatory = false)]
        public DateTime? LockExpirationAfter { get; set; }

        [Parameter(Mandatory = false)]
        public DateTime? LockExpirationBefore { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter DeserializeValues { get; set; }

        [Parameter(Mandatory = false)]
        public int? FirstResult { get; set; }

        [Parameter(Mandatory = false)]
        public int? MaxResults { get; set; }

        [Parameter(Mandatory = false)]
        public IEnumerable<Sorting<SortBy>> Sorting { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter Count { get; set; }

        

        [Parameter(Mandatory = false)]
        public CamundaClient Client { get; set; }

        
        protected override void ProcessRecord() {


            var exTasks = GlobalHelpers.GetCamundaClient(Client).ExternalTask;

            if (Count) {
                var result = exTasks.GetListCount(Options).GetAwaiter().GetResult();
                WriteObject(result.Count);
            } else {
                var result = exTasks.GetList(Options).GetAwaiter().GetResult();
                WriteObject(result, true);
            }

        }

        private void Options(GetListParameterBuilder options) {

            bool? locked = null;
            if (Locked.IsPresent) {
                locked = Locked.ToBool();
            }

            bool? withRetriesLeft = null;
            if (WithRetriesLeft.IsPresent)
            {
                withRetriesLeft = WithRetriesLeft.ToBool();
            }

            options.WithExternalTaskId(Id)
                .WithTopicName(TopicName)
                .WithWorkerId(WorkerId)
                .Locked(locked)
                .WithRetriesLeft(withRetriesLeft)
                .LockExpirationAfter(LockExpirationAfter)
                .LockExpirationBefore(LockExpirationBefore)
                .WithActivityId(ActivityId)
                .WithActivityIdIn(ActivityIdIn)
                .WithExecutionId(ExecutionId)
                .WithProcessInstanceId(ProcessInstanceId)
                .WithProcessDefinitionId(ProcessDefinitionId)
                .WithTenantId(TenantIdIn)
                .OnlyActive(OnlyActive)
                .OnlyPriorityHigherThanOrEquals(PriorityHigherThanOrEquals)
                .OnlyPriorityLowerThanOrEquals(PriorityLowerThanOrEquals)
                .OnlySuspended(OnlySuspended)
                .Sorting(Sorting)
                .FirstResultAtIndex(FirstResult)
                .MaxResultsToReturn(MaxResults)
                .DeserializeValues(DeserializeValues);



        }

    }
}
