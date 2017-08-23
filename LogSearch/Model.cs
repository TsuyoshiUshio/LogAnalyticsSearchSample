using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogSearch
{

    public class Rootobject
    {
        public string id { get; set; }
        public __Metadata __metadata { get; set; }
        public Value[] value { get; set; }
    }

    public class Resultobject
    {
        public string id { get; set; }
        public __Metadata __metadata { get; set; }
    }

    public class __Metadata
    {
        public string resultType { get; set; }
        public int total { get; set; }
        public int top { get; set; }
        public string RequestId { get; set; }
        public string Status { get; set; }
        public int NumberOfDocuments { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime LastUpdated { get; set; }
        public string ETag { get; set; }
        public Sort[] sort { get; set; }
        public int requestTime { get; set; }
    }

    public class Sort
    {
        public string name { get; set; }
        public string order { get; set; }
    }

    public class Value
    {
        public string SourceSystem { get; set; }
        public DateTime TimeGenerated { get; set; }
        public string Id_s { get; set; }
        public string ResourceGroupName_s { get; set; }
        public string DataFactoryName_s { get; set; }
        public string PipelineName_s { get; set; }
        public string ActivityName_s { get; set; }
        public string ActivityType_s { get; set; }
        public string State_s { get; set; }
        public string SubState_s { get; set; }
        public DateTime RunStart_t { get; set; }
        public DateTime RunEnd_t { get; set; }
        public DateTime WindowStart_t { get; set; }
        public DateTime WindowEnd_t { get; set; }
        public float PercentComplete_d { get; set; }
        public float RetryAttempt_d { get; set; }
        public float Rows_d { get; set; }
        public float Duration_d { get; set; }
        public string id { get; set; }
        public string Type { get; set; }
        public string MG { get; set; }
        public __Metadata1 __metadata { get; set; }
        public string RunId_s { get; set; }
        public string TableName_s { get; set; }
        public string Status_s { get; set; }
        public string HasLogs_s { get; set; }
        public string DataRead_s { get; set; }
        public string CopyDuration_s { get; set; }
        public string CopyThroughput_s { get; set; }
    }

    public class __Metadata1
    {
        public string Type { get; set; }
        public DateTime TimeGenerated { get; set; }
    }


}
