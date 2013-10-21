using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Fare;

namespace RegexTester.Controllers
{
    public class RegexController : Controller
    {
        [HttpGet]
        [ValidateInput(false)]
        public ActionResult Index(RegexViewModel viewModel)
        {
            return View(viewModel);
        }
    }

    public class RegexViewModel 
    {
        public RegexViewModel() { }
        public RegexViewModel(string regex, string textToMatch)
        {
            Regex = regex;
            TextToMatch = textToMatch;
        }

        public string Regex { get; set; }
        public IEnumerable<string> Examples
        { 
            get
            {
                if (_examples != null) return _examples;
                
                try
                {
                    _examples = Enumerable.Range(1, 500).Select(_ => new Xeger(Regex).Generate()).Distinct().Take(10).ToList();
                }
                catch (Exception)
                {
                    _examples = new string[0];
                }
                return _examples;
            }
        }
        IEnumerable<string> _examples;
        public string TextToMatch { get; set; }
        public MatchCollection Results
        { 
            get
            {
                if (_results != null) return _results;
                
                try
                {
                    _results = new Regex(Regex).Matches(TextToMatch);
                }
                catch (Exception e)
                {
                    _results = null;
                }
                return _results;
            }
        }
        MatchCollection _results;
    }
}
