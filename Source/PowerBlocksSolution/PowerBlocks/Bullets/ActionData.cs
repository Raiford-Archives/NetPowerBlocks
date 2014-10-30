using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PowerBlocks.Bullets
{


    class PlatformInfo
    {

        OS7
            Win7
            Linix

    }
    class ActionDataEnvelope  // or Packet
    {
        Guid Uid;
        ScheduleInfo schedule;
        int Prioty; // 1-100
        ProcessInfo; // seperate memory etc.
        Insulation



        ActionData ActionData;




    }

    interface IActionData
    {
        Execute(...)

        //optional for alater
        OnSuccess
            OnError
            OnCancel

    }

    class ActionDataBuilder
    {
        CreateFromObject<T>(T obj)

    
            void ExampalClient()
            {

                string


                ActionData.Execute()

            }

    }

    class ActionDataExecuter
    {
        // ProcessQueue
    }

    class ActionData
    {

        // what is dooes
        // - Execute w/ argument payload
        // - structuredData
        // - create object
        // - 

        string Type
        string AssemblyInfo
        string InterfaceName
        string InterfaceMethod
        ActionDataPayload  Payload; // packets of arguments passed to object   dictionary



        object ObjectData
        object 


    }
}



/