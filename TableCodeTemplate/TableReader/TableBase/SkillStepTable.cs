using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Kent.Boogaart.KBCsv;

namespace Tables
{
    public class SkillStepEvents
    {
        public float EventFrame { get; internal set; }
        public int EffectID { get; internal set; }
        public int BulletID { get; internal set; }
        public int TargetID { get; internal set; }
        public int[] PhysicScriptID { get; internal set; }
        public int SoundID { get; internal set; }
        public int HitPhysic { get; internal set; }
        public int HitLogic { get; internal set; }
        public int HitSoundID { get; internal set; }
        public int MissSoundID { get; internal set; }

        public SkillStepEvents()
        {
            PhysicScriptID = new []
            {
                new int(),
                new int(),
                new int(),
            };
        }
    }

    public class SkillStepTableRecord
    {
        public int Id { get; internal set; }
        public string Name { get; internal set; }
        public string AnimationName { get; internal set; }
        public float Speed { get; internal set; }
        public int WrapMode { get; internal set; }
        public string NextInput { get; internal set; }
        public float NextInputStartFrame { get; internal set; }
        public float NextInputEndFrame { get; internal set; }
        public float NextInputStartTime { get; internal set; }
        public float NextInputEndTime { get; internal set; }
        public float AutoNextFrame { get; internal set; }
        public float AutoNextTime { get; internal set; }
        public float FinishFrame { get; internal set; }
        public float FinishTime { get; internal set; }
        public SkillStepEvents[] SkillStepEvents { get; internal set; }

        public SkillStepTableRecord()
        {
            SkillStepEvents = new []
            {
                new SkillStepEvents(),
                new SkillStepEvents(),
                new SkillStepEvents(),
                new SkillStepEvents(),
                new SkillStepEvents(),
                new SkillStepEvents(),
                new SkillStepEvents(),
                new SkillStepEvents(),
                new SkillStepEvents(),
                new SkillStepEvents(),
            };
        }
    }

    public class SkillStepTable
    {
        public Dictionary<string, SkillStepTableRecord> Records { get; internal set; }

        public bool ContainsKey(string key)
        {
             return Records.ContainsKey(key);
        }

        public SkillStepTableRecord GetRecord(string id)
        {
            try
            {
                return Records[id];
            }
            catch (Exception ex)
            {
                throw new Exception("SkillStepTable" + ": " + id, ex);
            }
        }

        public SkillStepTable(string pathOrContent,bool isPath = true)
        {
            Records = new Dictionary<string, SkillStepTableRecord>();
            if(isPath)
             {
                 string[] lines = File.ReadAllLines(pathOrContent, Encoding.Default);
                 lines[0] = lines[0].Replace("\r\n", "\n");
                 ParserTableContent(string.Join("\n", lines));
             }
             else
             {
                 ParserTableContent(pathOrContent.Replace("\r\n", "\n"));
             }
        }
        private void ParserTableContent(string content)
        {
            using (var rdr = new StringReader(content))
            using (var reader = new CsvReader(rdr))
            {
                HeaderRecord header = reader.ReadHeaderRecord();
                const string tableName = "SkillStepTable";
                int lastId = -1;

                while (reader.HasMoreRecords)
                {
                    DataRecord data = reader.ReadDataRecord();
                    var r = new SkillStepTableRecord();
                    if (data[0].StartsWith("#"))
                        continue;


                    Records.Add(data[0], r);
                }
            }
        }
    }
}
