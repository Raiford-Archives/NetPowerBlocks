using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PowerBlocks.Bullets;

namespace PowerBlocks.Tests.Bullets
{
    [TestClass]
    public class ActionObjectTest
    {
        [TestMethod]
        public void Basic_Test()
        {
            // Create an object to store
            QueueCommand command = new QueueCommand();
            command.Name = "My Really Cool Command";
            command.Id = 222;
            command.Description = "This is a really cool description of my command object.";
            command.Category = "AWESOME";

 //           ObjectBuilder<QueueCommand> builder = new ObjectBuilder<QueueCommand>(command);

           ObjectBuilder builder = new ObjectBuilder(command);

           ActionObjectSerializer serializer = new ActionObjectSerializer();




            // Add your data
            builder.AddInnerObject(command);
            //builder.AddActionObject(actionObject);

            ActionObject actionObject = new ActionObject(command);
            


            // To Jsonb
            string json = builder.ToJson();
            //json = serializer.Serialize(actionObject);
            json = ActionObjectSerializer.Serialize(actionObject);

            // Convert to various formats
            //builder.ToActionObject();
            //builder.ToInnerObject();
            //builder.ToEncryptedString();
            //builder.ToJson();
            //builder.ToString(); // same as json

            //builder.GetActionObject();
            //builder.GetInnerObject();
            QueueCommand innerObject = ActionObjectSerializer.DeserializeInner<QueueCommand>(json);

            ActionObject obj = ActionObjectSerializer.Deserialize(json);




          






           // ActionObject ob


        }
    }
}
