using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using System.Threading.Tasks;
using doob.Camunda.Client;
using doob.Camunda.Client.ProcessDefinition;

namespace Camunda.PsClient.ProcessDefinitions
{
    [Cmdlet(VerbsCommon.Get, "CamundaActivityInstanceStatistics", DefaultParameterSetName = "ById")]
    public class GetCamundaActivityInstanceStatistics : PSCmdlet
    {
       
        [Parameter(Mandatory = true, ParameterSetName = "ById", Position = 0)]
        public string Id { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "ByKey", Position = 0)]
        public string Key { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = "ByKey", Position = 1)]
        public string TenantId { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter IncludeFailedJobs { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter IncludeIncidents { get; set; }

        [Parameter(Mandatory = false)]
        public string IncludeIncidentsForType { get; set; }

        [Parameter(Mandatory = false)]
        public CamundaClient Client { get; set; }

        protected override void ProcessRecord() {


            Task<List<ActivityInstanceStatisticsObject>> req = null;

            if (this.ParameterSetName == "ById") {
                 req = GlobalHelpers.GetCamundaClient(Client).ProcessDefinition.GetActivityInstanceStatisticsById(Id, Options);
            } else {
                req = GlobalHelpers.GetCamundaClient(Client).ProcessDefinition.GetActivityInstanceStatisticsByKey(Key, TenantId, Options);
            }

            var result = req.GetAwaiter().GetResult();

            WriteObject(result);

        }

        private void Options(ActivityInstanceStatisticsQueryParametersBuilder parameters) {

            parameters.IncludeFailedJobs(IncludeFailedJobs);
            parameters.IncludeIncidents(IncludeIncidents);
            parameters.IncludeIncidentsForType(IncludeIncidentsForType);

        }

       
    }
}
