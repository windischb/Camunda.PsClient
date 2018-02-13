using System;
using System.Management.Automation;
using doob.Camunda.Client;
using doob.Camunda.Client.Deployment;
using doob.Camunda.Client.Global;
using doob.Reflectensions;

namespace Camunda.PsClient.Deployment {

    [Cmdlet(VerbsCommon.Get, "CamundaDeployment")]
    public class GetCamundaDeployment : PSCmdlet {
        [Parameter(Mandatory = false)]
        public string Id { get; set; }

        [Parameter(Mandatory = false)]
        public string Name { get; set; }

        [Parameter(Mandatory = false)]
        public string Source { get; set; }

        [Parameter(Mandatory = false)]
        public string[] TentantId { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter IncludeDeploymentsWithoutTenantId { get; set; }

        [Parameter(Mandatory = false)]
        public DateTime? After { get; set; }

        [Parameter(Mandatory = false)]
        public DateTime? Before { get; set; }

        [Parameter(Mandatory = false)]
        public SortBy? SortBy { get; set; }

        [Parameter(Mandatory = false)]
        public SortOrder? SortOrder { get; set; }

        [Parameter(Mandatory = false)]
        public int? FirstResult { get; set; }

        [Parameter(Mandatory = false)]
        public int? MaxResults { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter Count { get; set; }

        [Parameter(Mandatory = false)]
        public CamundaClient Client { get; set; }


        protected override void ProcessRecord() {

            var deployment = GlobalHelpers.GetCamundaClient(Client).Deployment;
            var options = GetOptions();

            if (Count) {

                var result = deployment.GetListCount(options).GetAwaiter().GetResult();
                WriteObject(result);

            } else {
                var result = deployment.GetList(options).GetAwaiter().GetResult();
                WriteObject(result, true);
            }

        }

        private GetListOptionsBuilder GetOptions() {

            var optionsBuilder = new GetListOptionsBuilder();
            bool withoutsource = this.MyInvocation.BoundParameters.ContainsKey(nameof(Source)) && Source.ToNull() == null;
            bool withoutTenantId = this.MyInvocation.BoundParameters.ContainsKey(nameof(TentantId)) && (TentantId == null || TentantId.Length == 0);

            optionsBuilder.WithId(Id)
                .WithName(Name)
                .WithSource(Source)
                .WithEmptySource(withoutsource)
                .WithTenantId(TentantId)
                .OnlyWithEmptyTenantId(withoutTenantId)
                .IncludeEmptyTenantId(IncludeDeploymentsWithoutTenantId)
                .DeployedAfter(After)
                .DeployedBefore(Before)
                .SortBy(SortBy)
                .SortOrder(SortOrder)
                .FirstResultAtIndex(FirstResult)
                .MaxResultsToReturn(MaxResults);

            return optionsBuilder;
        }

    }
}
