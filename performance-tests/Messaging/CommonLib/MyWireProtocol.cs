using System.Text;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Communication.Protocols.BinarySerialization;

namespace CommonLib
{
    /// <summary>
    /// This class is a sample custom wire protocol to use as wire protocol in SCS framework.
    /// It extends BinarySerializationProtocol.
    /// It is used just to send/receive ScsTextMessage messages.
    /// 
    /// Since BinarySerializationProtocol automatically writes message length to the beggining
    /// of the message, a message format of this class is:
    /// 
    /// [Message length (4 bytes)][UTF-8 encoded text (N bytes)]
    /// 
    /// So, total length of the message = (N + 4) bytes;
    /// </summary>
    public class MyWireProtocol : BinarySerializationProtocol
    {
        /// <summary>
        /// The sender of a message needs to provide a means to convert a (high-level) object into raw bytes
        /// that will be sent over the network connection.
        /// </summary>
        protected override byte[] SerializeMessage(IScsMessage message)
        {
            if (message is ScsRawDataMessage)
            {
                return (((ScsRawDataMessage)message).MessageData);
            } 

            if (message is ScsTextMessage)
            {
                return Encoding.UTF8.GetBytes(((ScsTextMessage)message).Text);
            }  
            
            if (message is ScsPingMessage)
            {
                //if the application idles, occasional ScsPingMessage(s) are being sent out
                //trying to serialize this as ScsTextMessage will throw an exception
            }

            return null;
        }

        /// <summary>
        /// The receiver of a network message gets raw bytes and converts these into a (high-level) object.
        /// You could try to deserialize into every single "high-level" message class that you have defined 
        /// for communication, catching deserialization exceptions along the way until you arrive at a message 
        /// that correctly deserializes without exception, but exceptions like this are EXTREMELY costly, 
        /// so this is usually not the way you want it to go. A common approach in game server programming
        /// uses a simple id as the first (few) bytes of the message to uniquely identify the (high-level) 
        /// message. You'd read this byte(s) and then generate your message using a switch-case statement.
        /// 
        /// We don't use any of the aforementioned methods here though, as we're solely interested in network
        /// performance without the added serialization performance overhead.
        /// </summary>
        protected override IScsMessage DeserializeMessage(byte[] bytes)
        {
            //Decode UTF8 encoded text and create a ScsTextMessage object
            return new ScsTextMessage(Encoding.UTF8.GetString(bytes));
            
            //we'll just pass the bytes array up in our high-level message.
            //return new ScsRawDataMessage(bytes);
        }
    }
}
