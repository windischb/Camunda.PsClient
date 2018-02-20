using System;
using System.Management.Automation;
using doob.Camunda.Client;
using doob.Camunda.Client.Tasks;
using doob.Reflectensions;

namespace Camunda.PsClient.Tasks
{

    [Cmdlet(VerbsCommon.Get, "CamundaTask")]
    public class GetCamundaTask : PSCmdlet
    {
        [Parameter(Mandatory = false)]
        public string Id { get; set; }

        [Parameter(Mandatory = false)]
        public string ProcessInstanceId { get; set; }

        [Parameter(Mandatory = false)]
        public string ProcessInstanceBusinessKey { get; set; }

        [Parameter(Mandatory = false)]
        public string ProcessInstanceBusinessKeyExpression { get; set; }

        [Parameter(Mandatory = false)]
        public string[] ProcessInstanceBusinessKeyIn { get; set; }


        [Parameter(Mandatory = false)]
        public string ProcessDefinitionId { get; set; }

        [Parameter(Mandatory = false)]
        public string ProcessDefinitionKey { get; set; }

        [Parameter(Mandatory = false)]
        public string[] ProcessDefinitionKeyIn { get; set; }

        [Parameter(Mandatory = false)]
        public string ProcessDefinitionName { get; set; }


        [Parameter(Mandatory = false)]
        public string ExecutionId { get; set; }

        [Parameter(Mandatory = false)]
        public string CaseInstanceId { get; set; }

        [Parameter(Mandatory = false)]
        public string CaseInstanceBusinessKey { get; set; }

        [Parameter(Mandatory = false)]
        public string CaseDefinitionId { get; set; }

        [Parameter(Mandatory = false)]
        public string CaseDefinitionKey { get; set; }

        [Parameter(Mandatory = false)]
        public string CaseDefinitionName { get; set; }

        [Parameter(Mandatory = false)]
        public string CaseExecutionId { get; set; }

        [Parameter(Mandatory = false)]
        public string[] ActivityInstanceIdIn { get; set; }

        [Parameter(Mandatory = false)]
        public string[] TenantIdIn { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter OnlyWithEmptyTenantId { get; set; }

        [Parameter(Mandatory = false)]
        public string Assignee { get; set; }

        [Parameter(Mandatory = false)]
        public string AssigneeExpression { get; set; }

        [Parameter(Mandatory = false)]
        public string Owner { get; set; }

        [Parameter(Mandatory = false)]
        public string OwnerExpression { get; set; }

        [Parameter(Mandatory = false)]
        public string CandidateGroup { get; set; }

        [Parameter(Mandatory = false)]
        public string CandidateGroupExpression { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter HaveCandidateGroups { get; set; }

        [Parameter(Mandatory = false)]
        public string CandidateUser { get; set; }

        [Parameter(Mandatory = false)]
        public string CandidateUserExpression { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter HaveCandidateUsers { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter IncludeAssignedTasks { get; set; }

        [Parameter(Mandatory = false)]
        public string InvolvedUser { get; set; }

        [Parameter(Mandatory = false)]
        public string InvolvedUserExpression { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter Assigned { get; set; }

        [Parameter(Mandatory = false)]
        public string TaskDefinitionKey { get; set; }

        [Parameter(Mandatory = false)]
        public string[] TaskDefinitionKeyIn { get; set; }

        [Parameter(Mandatory = false)]
        public string Name { get; set; }

        [Parameter(Mandatory = false)]
        public string NotName { get; set; }

        [Parameter(Mandatory = false)]
        public string Description { get; set; }

        [Parameter(Mandatory = false)]
        public int? Priority { get; set; }

        [Parameter(Mandatory = false)]
        public int? MinPriority { get; set; }

        [Parameter(Mandatory = false)]
        public int? MaxPriority { get; set; }



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

        //[Parameter(Mandatory = false)]
        //public IEnumerable<Sorting<SortBy>> Sorting { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter Count { get; set; }

        

        [Parameter(Mandatory = false)]
        public CamundaClient Client { get; set; }

        
        protected override void ProcessRecord() {


            var tasks = GlobalHelpers.GetCamundaClient(Client).Task;

           
            if (Count)
            {

                if (Id.ToNull() != null)
                {
                    var result = tasks.Get(Id).GetAwaiter().GetResult();
                    if(result != null)
                        WriteObject(1);
                }
                else
                {
                    ///TODO: Enable after update to new Camunda.Client Version
                    //var result = tasks.GetListCount(Options).GetAwaiter().GetResult();
                    //WriteObject(result.Count);
                }

            }
            else
            {
                if (Id.ToNull() != null)
                {
                    var result = tasks.Get(Id).GetAwaiter().GetResult();
                    WriteObject(result);
                }
                else
                {
                    var result = tasks.GetList(Options).GetAwaiter().GetResult();
                    WriteObject(result, true);
                }
            }

        }

        private void Options(GetListOptionsBuilder options) {

            bool? locked = null;
            if (Locked.IsPresent) {
                locked = Locked.ToBool();
            }

            bool? withRetriesLeft = null;
            if (WithRetriesLeft.IsPresent)
            {
                withRetriesLeft = WithRetriesLeft.ToBool();
            }

            options
                .WithProcessInstanceId(ProcessInstanceId)
                .WithProcessInstanceBusinessKey(ProcessInstanceBusinessKey)
                .WithProcessInstanceBusinessKeyExpression(ProcessInstanceBusinessKeyExpression)
                .WithProcessInstanceBusinessKeyIn(ProcessInstanceBusinessKeyIn)

                .WithProcessDefinitionId(ProcessDefinitionId)
                .WithProcessDefinitionKey(ProcessDefinitionKey)
                .WithProcessDefinitionKeyIn(ProcessDefinitionKeyIn)
                .WithProcessDefinitionName(ProcessDefinitionName)

                .WithExecutionId(ExecutionId)

                .WithCaseInstanceId(CaseInstanceId)
                .WithCaseInstanceBusinessKey(CaseInstanceBusinessKey)
                .WithCaseDefinitionId(CaseDefinitionId)
                .WithCaseDefinitionKey(CaseDefinitionKey)
                .WithCaseDefinitionName(CaseDefinitionName)
                .WithCaseExecutionId(CaseExecutionId)
                .WithActivityInstanceIdIn(ActivityInstanceIdIn)
                .WithTenantId(TenantIdIn)
                .OnlyWithEmptyTenantId(OnlyWithEmptyTenantId)
                .WithAssignee(Assignee)
                .WithAssigneeExpression(AssigneeExpression)
                .WithOwner(Owner)
                .WithOwnerExpression(OwnerExpression)
                .WithCandidateGroup(CandidateGroup)
                .WithCandidateGroupExpression(CandidateGroupExpression)
                .WithHaveCandidateGroups(HaveCandidateGroups)
                .WithCandidateUser(CandidateUser)
                .WithCandidateUserExpression(CandidateUserExpression)
                .WithHaveCandidateUsers(HaveCandidateUsers)
                .IncludeAssignedTasks(IncludeAssignedTasks)
                .WithInvolvedUser(InvolvedUser)
                .WithInvolvedUserExpression(InvolvedUserExpression)
                .Assigned(Assigned)
                .WithTaskDefinitionKey(TaskDefinitionKey)
                .WithTaskDefinitionKeyIn(TaskDefinitionKeyIn)
                .WithName(Name)
                .WithNotName(NotName)
                .WithDescription(Description)
                .WithPriority(Priority)
                .WithMinPriority(MinPriority)
                .WithMaxPriority(MaxPriority)
                ;

        }

    }
}
