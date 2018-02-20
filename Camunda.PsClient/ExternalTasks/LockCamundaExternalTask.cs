using System;
using System.Collections.Generic;
using System.Management.Automation;
using doob.Camunda.Client;
using doob.Camunda.Client.ExternalTasks;

namespace Camunda.PsClient.ExternalTasks
{

    [Cmdlet(VerbsCommon.Lock, "CamundaExternalTask")]
    public class LockCamundaExternalTask : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public string WorkerId { get; set; }

        [Parameter(Mandatory = false)]
        public int MaxTasks { get; set; } = 1;

        [Parameter(Mandatory = false)]
        public SwitchParameter UsePriority { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = "Topics")]
        public IEnumerable<FetchAndLockTopic> Topics { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = "Topic", ValueFromPipelineByPropertyName = true)]
        public string TopicName { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = "Topic")]
        public DateTime? LockUntil { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = "Topic")]
        public string[] Variables { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = "Topic")]
        public SwitchParameter DeserializeValues { get; set; }

        [Parameter(Mandatory = false)]
        public CamundaClient Client { get; set; }

        
        protected override void ProcessRecord() {


            var exTasks = GlobalHelpers.GetCamundaClient(Client).ExternalTask;

            
                var result = exTasks.FetchAndLock(Options).GetAwaiter().GetResult();
                WriteObject(result, true);

        }

        private void Options(FetchAndLockParametersBuilder options) {
            options.UsePriority(UsePriority);
            options.WithWorkerId(WorkerId);
            options.MaxTasks(MaxTasks);
            
            if (ParameterSetName == "Topic") {
                var ts = LockUntil != null ? LockUntil.Value - DateTime.Now : TimeSpan.FromMinutes(10);

                options.AddTopic(TopicName, ts, Variables, DeserializeValues);
            } else {
                if (Topics != null)
                {
                    foreach (var fetchAndLockTopic in Topics)
                    {
                        options.AddTopic(fetchAndLockTopic);
                    }
                }
            }

            
            
        }

    }
}
