using Amazon.Lambda.Core;
using Newtonsoft.Json;
using Slight.Alexa.Framework.Models.Requests;
using Slight.Alexa.Framework.Models.Responses;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using System.Collections.Generic;
using System.IO;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializerAttribute(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace AlexaMyChevy
{
    public class AlexaLIsteningToMe
    {   string textForLog = string.Empty;

        ILambdaLogger log;
        public async Task<SkillResponse> FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            Response response;
            IOutputSpeech innerResponse = null;
            log = context.Logger;
            bool shouldEndSession = true;
            textForLog = "";
            string intentName = "";
            try {


                log.LogLine("1");
                innerResponse = new PlainTextOutputSpeech();
                (innerResponse as PlainTextOutputSpeech).Text = "Hi,  Just keep speaking and ask alexa to stop when you are done.";

                log.LogLine("2");
                // intent request, process the intent
                //log.LogLine($"Intent Requested {input.Request.Intent.Name}");
                try
                {
                    intentName = input.Request.Intent.Name;
                } catch
                {
                    try
                    {
                        intentName = input.Request.Type;
                    } catch
                    {
                        intentName = "-no request-";
                    }
                }
                
                log.LogLine("3");
                log.LogLine("intentName:" + intentName);
                textForLog += Environment.NewLine + "INTENT NAME:";
                textForLog += intentName;
                textForLog += ": BEFORE SWITCH";
                switch (intentName)  {
                    //case "AMAZON.HelpIntent":
                    //    (innerResponse as PlainTextOutputSpeech).Text = "Welcome to Pick My Game.  Please try Alexa, Ask Pick My Game to decide for me";
                    //    break;
                    case "AMAZON.StopIntent":
                        (innerResponse as PlainTextOutputSpeech).Text = "";
                        shouldEndSession = true;
                        break;
                    case "AMAZON.CancelIntent":
                        (innerResponse as PlainTextOutputSpeech).Text = "";
                        shouldEndSession = true;
                        break;
                    //case "AMAZON.NextIntent":
                    //    (innerResponse as PlainTextOutputSpeech).Text = "Welcome to Pick My Game.  Please try Alexa, Ask Pick My Game to decide for me";
                    //    break;
                    //case "AMAZON.NoIntent":
                    //    (innerResponse as PlainTextOutputSpeech).Text = "OK";
                    //    break;
                    //case "AMAZON.PreviousIntent":
                    //    (innerResponse as PlainTextOutputSpeech).Text = "OK";
                    //    break;
                    //case "AMAZON.RepeatIntent":
                    //    (innerResponse as PlainTextOutputSpeech).Text = "OK";
                    //    break;
                    //case "AMAZON.ResumeIntent":
                    //    (innerResponse as PlainTextOutputSpeech).Text = "OK";
                    //    break;
                    //case "AMAZON.YesIntent":
                    //    (innerResponse as PlainTextOutputSpeech).Text = "OK";
                    //    break;
                    case "StopIntent":
                        (innerResponse as PlainTextOutputSpeech).Text = "";
                        shouldEndSession = true;
                        break;
                    case "CancelIntent":
                        (innerResponse as PlainTextOutputSpeech).Text = "";
                        shouldEndSession = true;
                        break;
                    case "LaunchRequest":
                        (innerResponse as PlainTextOutputSpeech).Text = "Hi, let me listen to you.  Just keep speaking and ask alexa to stop when you are done.";
                        shouldEndSession = false;
                        break;
                    //case "HelpIntent":
                    //    //(innerResponse as PlainTextOutputSpeech).Text = "Welcome to Pick My Game.  Please try Alexa, Ask Pick My Game to decide for me";
                    //    shouldEndSession = false;
                    //    break;
                    case "UnknownIntent":
                        (innerResponse as PlainTextOutputSpeech).Text = "Uh huh";
                        break;
                    case "ListenIntent":
                        (innerResponse as PlainTextOutputSpeech).Text = "Uh huh";
                        shouldEndSession = false;
                        break;
                    default:
                        (innerResponse as PlainTextOutputSpeech).Text = "Uh huh";
                        shouldEndSession = false;
                        break;
                 }

                //}

            }
            catch (Exception ex)
            {
                textForLog += Environment.NewLine + ex.Message + Environment.NewLine + textForLog + Environment.NewLine + ex.StackTrace.ToString();
            }
            //if (textForLog != string.Empty) { await SaveTextToLog(textForLog, input); }
            response = new Response();
            response.ShouldEndSession = shouldEndSession;
            response.OutputSpeech = innerResponse;
            SkillResponse skillResponse = new SkillResponse();
            skillResponse.Response = response;
            skillResponse.Version = "1.0";
            skillResponse.SessionAttributes = new System.Collections.Generic.Dictionary<string, object>();
            
            return skillResponse;
        }
        
        private async Task<bool> SaveTextToLog(string text, SkillRequest input)
        {
            log.LogLine(text);
            //try
            //{
            //    var dbclientlog = new AmazonDynamoDBClient("AKIAIBE7ZU4BVJEPGXAQ", "XvyOVgZ7UtUMTzqYpVMOkJukHb2ABaYPBFPEqzni");
            //    Table tablelog = Table.LoadTable(dbclientlog, "JimAlexaLog");
            //    var newdoc = Document.FromJson(Newtonsoft.Json.JsonConvert.SerializeObject(input));
            //    newdoc.Add("RowPointer", Guid.NewGuid().ToString());
            //    newdoc.Add("RecordTIme", DateTime.Now);
            //    newdoc.Add("Text", text);
            //    log.LogLine(newdoc.ToJsonPretty());
            //    await tablelog.PutItemAsync(newdoc);
            //}
            //catch (Exception ex)
            //{
            //    log.LogLine(ex.Message);
            //}
            return true;
        }
    }
}
