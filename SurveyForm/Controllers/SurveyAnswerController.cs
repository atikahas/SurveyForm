using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SurveyForm.Dto;
using SurveyForm.Interfaces;
using SurveyForm.Models;

namespace SurveyForm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyAnswerController : Controller
    {
        private readonly ISurveyAnswerRepository _surveyAnswerRepository;
        private readonly IMapper _mapper;
        private readonly ISurveyQuestionRepository _surveyQuestionRepository;
        private readonly ISurveyRepository _surveyRepository;

        public SurveyAnswerController(ISurveyAnswerRepository surveyAnswerRepository,
            IMapper mapper,
            ISurveyQuestionRepository surveyQuestionRepository,
            ISurveyRepository surveyRepository)
        {
            _surveyAnswerRepository = surveyAnswerRepository;
            _mapper = mapper;
            _surveyQuestionRepository = surveyQuestionRepository;
            _surveyRepository = surveyRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SurveyAnswer>))]
        public IActionResult GetSurveyAnswers()
        {
            var answers = _mapper.Map<List<SurveyAnswerDto>>(_surveyAnswerRepository.GetSurveyAnswers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(answers);
        }

        [HttpGet("{ansid}")]
        [ProducesResponseType(200, Type = typeof(SurveyAnswer))]
        [ProducesResponseType(400)]
        public IActionResult GetSurveyAnswer(int ansid)
        {
            if (!_surveyAnswerRepository.SurveyAnswerExists(ansid))
                return NotFound();

            var answer = _mapper.Map<SurveyAnswerDto>(_surveyAnswerRepository.GetSurveyAnswer(ansid));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(answer);
        }

        [HttpGet("question/{questId}")]
        [ProducesResponseType(200, Type = typeof(SurveyAnswer))]
        [ProducesResponseType(400)]
        public IActionResult GetQuestion(int questId)
        {
            var reviews = _mapper.Map<List<SurveyAnswerDto>>(_surveyAnswerRepository.GetQuestion(questId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(reviews);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateSurveyAnswer([FromQuery] int surveyId, [FromQuery] int questId, [FromBody] SurveyAnswerDto surveyAnswerCreate)
        {
            if (surveyAnswerCreate == null)
                return BadRequest(ModelState);

            var answers = _surveyAnswerRepository.GetSurveyAnswers()
                .Where(c => c.Answer.Trim().ToUpper() == surveyAnswerCreate.Answer.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (answers != null)
            {
                ModelState.AddModelError("", "Answer already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var answerMap = _mapper.Map<SurveyAnswer>(surveyAnswerCreate);

            answerMap.SurveyQuestion = _surveyQuestionRepository.GetSurveyQuestion(questId);
            answerMap.Survey = _surveyRepository.GetSurvey(surveyId);


            if (!_surveyAnswerRepository.CreateSurveyAnswer(answerMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Answer created");
        }
    }
}
