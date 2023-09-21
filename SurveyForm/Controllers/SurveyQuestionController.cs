using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SurveyForm.Dto;
using SurveyForm.Interfaces;
using SurveyForm.Models;

namespace SurveyForm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyQuestionController : Controller
    {
        private readonly ISurveyQuestionRepository _surveyQuestionRepository;
        private readonly IMapper _mapper;

        public SurveyQuestionController(ISurveyQuestionRepository surveyQuestionRepository, IMapper mapper)
        {
            _surveyQuestionRepository = surveyQuestionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SurveyQuestion>))]
        public IActionResult GetSurveyQuestions()
        {
            var surveyquestions = _mapper.Map<List<SurveyQuestionDto>>(_surveyQuestionRepository.GetSurveyQuestions());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(surveyquestions);
        }

        [HttpGet("{questid}")]
        [ProducesResponseType(200, Type = typeof(SurveyQuestion))]
        [ProducesResponseType(400)]

        public IActionResult GetSurveyQuestion(int questid)
        {
            if (!_surveyQuestionRepository.SurveyQuestionExists(questid))
                return NotFound();

            var surveyquestion = _mapper.Map<SurveyQuestionDto>(_surveyQuestionRepository.GetSurveyQuestion(questid));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(surveyquestion);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateSurveyQuestion([FromBody] SurveyQuestionDto surveyQuestionCreate)
        {
            if (surveyQuestionCreate == null)
                return BadRequest(ModelState);

            var quest = _surveyQuestionRepository.GetSurveyQuestions()
                .Where(q => q.Question.Trim().ToUpper() == surveyQuestionCreate.Question.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (quest != null)
            {
                ModelState.AddModelError("", "Question already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var questMap = _mapper.Map<SurveyQuestion>(surveyQuestionCreate);

            if (!_surveyQuestionRepository.CreateSurveyQuestion(questMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Question created");
        }
    }
}
