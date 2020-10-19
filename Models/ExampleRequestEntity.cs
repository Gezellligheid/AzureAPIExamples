using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Cosmos.Table;

namespace FunctionsExamples.Models
{

    //Has to extend TableEntity
    //Import the Microsoft.Azure.Cosmos.Table version of TableEntity
    class ExampleRequestEntity : TableEntity
    {

        public ExampleRequestEntity()
        {
            //Can be changed to whatevery you want
            PartitionKey = age.ToString();
            RowKey = new Guid().ToString(); // Has to be unique for each row you add

        }

        public string name { get; set; }
        public string address { get; set; }
        public int age { get; set; }

    }
}
