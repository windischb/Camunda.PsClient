using System.Collections;
using System.Management.Automation;
using doob.Camunda.Client;
using doob.Camunda.Client.ProcessDefinition;

namespace Camunda.PsClient.ProcessDefinitions
{
    [Cmdlet(VerbsCommon.New, "CamundaProcessStartInstructions")]
    public class NewCamundaProcessStartInstructions : PSCmdlet
    {
       
        [Parameter(Mandatory = true, Position = 0)]
        public StartInstructionType Type { get; set; }

        [Parameter(Mandatory = false)]
        public Hashtable Variables { get; set; }

        [Parameter(Mandatory = false)]
        public string ActivityId { get; set; }

        [Parameter(Mandatory = false)]
        public string TransitionId { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter AsGlobalVariables { get; set; }


        [Parameter(Mandatory = false)]
        public CamundaClient Client { get; set; }

        protected override void ProcessRecord() {

           var instr = new StartInstructionPropertiesBuilder();

            instr.WithType(Type);
            instr.WithActivityId(ActivityId);
            instr.WithTransitionId(TransitionId);

            if (Variables != null)
            {
                foreach (var key in Variables.Keys)
                {
                    instr.AddVariable(key.ToString(), Variables[key.ToString()], !AsGlobalVariables);
                }
            }

            WriteObject((StartInstructionProperties)instr);
        }

       
       
    }
}
