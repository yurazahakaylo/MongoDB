using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Repository
{
    public class neo
    {
        private GraphClient _client;


        public neo()
        {
            _client = new GraphClient(new Uri("http://localhost:7474/db/data/"), "neo4j", "123456");
            _client.Connect();
        }

        public void Create(string username)
        {
            _client.Cypher
                .Create("(n:Person { username: {usern} })")
                .WithParam("usern", username)
                .ExecuteWithoutResults();
        }
        

    }
}
