using System;
using System.Linq;
using MobilePoll.Application.Events;
using MobilePoll.DataModel;

namespace MobilePoll.Application.Parsers
{
    public class FreeformQuestionParser : QuestionParser
    {
        protected override string Type
        {
            get { return "Freeform"; }
        }

        protected override bool IsMulipleOptionQuestion
        {
            get { return false; }
        }

        protected override void ExtractData(Guid surveyId, string surveyName, SurveyQuestion question)
        {
            string answer = question.Answers.First();

            var answerReceived = new FreeFormAnswerReceived
            {
                SurveyId = surveyId,
                SurveyName = surveyName,
                Question = question.Question,
                QuestionId = question.QuestionNumber,
                Answer = answer,
            };

            Bus.Raise(answerReceived);
        }
    }
}