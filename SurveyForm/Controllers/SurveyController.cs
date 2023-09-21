using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SurveyForm.Dto;
using SurveyForm.Interfaces;
using SurveyForm.Models;

namespace SurveyForm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : Controller 
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly IMapper _mapper;
        public SurveyController(ISurveyRepository surveyRepository, IMapper mapper)
        {
            _surveyRepository = surveyRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Survey>))]
        public IActionResult GetSurveys()
        {
            var surveys = _mapper.Map<List<SurveyDto>>(_surveyRepository.GetSurveys());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(surveys);
        }

        [HttpGet("{survId}")]
        [ProducesResponseType(200, Type = typeof(Survey))]
        [ProducesResponseType(400)]

        public IActionResult GetSurvey(int survId)
        {
            if (!_surveyRepository.SurveyExists(survId))
                return NotFound();

            var survey = _mapper.Map<SurveyDto>(_surveyRepository.GetSurvey(survId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(survey);
        }
    }
}
