using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Kent.Boogaart.KBCsv;

namespace Tables
{
    public class SkillInfoTableRecord
    {
        public DataRecord ValueStr;

        public string Id { get; internal set; }
        public string Name { get; internal set; }
        public int SkillGroup { get; internal set; }
        public SKILL_PRIOR_TYPE PriorType { get; internal set; }
        public float CDTime { get; internal set; }
        public List<SkillStepTableRecord> Step { get; internal set; }

        public SkillInfoTableRecord(DataRecord dataRecord)
        {
            ValueStr = dataRecord;
            Id = ValueStr[0];

            Step = new List<SkillStepTableRecord>();
        }
    }

    public partial class SkillInfoTable
    {
        public Dictionary<string, SkillInfoTableRecord> Records { get; internal set; }

        public bool ContainsKey(string key)
        {
             return Records.ContainsKey(key);
        }

        public SkillInfoTableRecord GetRecord(string id)
        {
            try
            {
                return Records[id];
            }
            catch (Exception ex)
            {
                throw new Exception("SkillInfoTable" + ": " + id, ex);
            }
        }

        public SkillInfoTable(string pathOrContent,bool isPath = true)
        {
            Records = new Dictionary<string, SkillInfoTableRecord>();
            if(isPath)
            {
                string[] lines = File.ReadAllLines(pathOrContent, Encoding.Default);
                lines[0] = lines[0].Replace("\r\n", "\n");
                ParserTableStr(string.Join("\n", lines));
            }
            else
            {
                ParserTableStr(pathOrContent.Replace("\r\n", "\n"));
            }
        }

        private void ParserTableStr(string content)
        {
            StringReader rdr = new StringReader(content);
            using (var reader = new CsvReader(rdr))
            {
                HeaderRecord header = reader.ReadHeaderRecord();
                while (reader.HasMoreRecords)
                {
                    DataRecord data = reader.ReadDataRecord();
                    if (data[0].StartsWith("#"))
                        continue;

                    SkillInfoTableRecord record = new SkillInfoTableRecord(data);
                    Records.Add(record.Id, record);
                }

            }
        }

        public void CoverTableContent()
        {
            foreach (var pair in Records)
            {
                pair.Value.Name = pair.Value.ValueStr[1];
                pair.Value.SkillGroup = TableReadBase.ParseInt(pair.Value.ValueStr[2]);
                pair.Value.PriorType = (SKILL_PRIOR_TYPE)TableReadBase.ParseInt(pair.Value.ValueStr[3]);
                pair.Value.CDTime = TableReadBase.ParseInt(pair.Value.ValueStr[4]);

                for (int i = 0; i < 3; ++i)
                {
                    if (pair.Value.ValueStr[i + 5] != "-1")
                    {
                        pair.Value.Step.Add(TableReader.SkillStepTable.GetRecord(pair.Value.ValueStr[i + 5]));
                    }
                }
            }
            
        }
    }
}
