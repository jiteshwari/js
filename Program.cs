using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace ClsSerialization
{
    
       [Serializable()]
       public class NonSerialized
	   {
		  public int num;
		  public string name;
		  public string address;

		[NonSerialized]		
		public double Age;
        public NonSerialized()
        {
            this.num = 11;
            this.name = "js";
            this.address = "Blr";
            this.Age = 21;
        }
		public void Display()
        {
            Console.WriteLine("Num:"  ,num);
            Console.WriteLine("Name:" , name);
            Console.WriteLine("Address:" , address);
            Console.WriteLine("Age: " , Age);
        }
    }
	public class Program
    {
		static void Main(string[] args)
		{
			NonSerialized nonSerialized = new NonSerialized();
			Console.WriteLine("Before Serilization: ");
			nonSerialized.Display();
			Console.WriteLine();

			//Binary Format - serializer
			Stream stream = File.Open(@"D:\biz-pro\jitesh.txt", FileMode.Create);
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(stream, nonSerialized);
			stream.Close();
			nonSerialized = null;

			//Binary Format - Deserializer
			stream = new FileStream(@"D:\biz-pro\jitesh.txt", FileMode.Open);
			binaryFormatter = new BinaryFormatter();
			nonSerialized = (NonSerialized)binaryFormatter.Deserialize(stream);
			stream.Close();

			Console.WriteLine("After Binary Serialization: ");
			nonSerialized.Display();
			Console.WriteLine();

            // XmlSerialiser
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(NonSerialized));
            TextWriter textWriter = new StreamWriter(@"D:\biz-pro\jitesh.xml");
            xmlSerializer.Serialize(textWriter, nonSerialized);
            textWriter.Close();
            nonSerialized = null;
            
            // Deserializer
            TextReader textReader = new StreamReader(@"D:\biz-pro\jitesh.xml");
            xmlSerializer = new XmlSerializer(typeof(NonSerialized));
            Object obj = xmlSerializer.Deserialize(textReader);
            nonSerialized = (NonSerialized)obj;
            Console.WriteLine("After Xml Serialization ");
            nonSerialized.Display();
            textReader.Close();

            Console.Read();
		}
	}
} 