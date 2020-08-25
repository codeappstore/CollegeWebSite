using College.Access.IRepository;
using College.Helpers;
using College.Model.DataTransferObject.FooterDto;
using College.Model.DataTransferObject.ImportantLinksDto;
using College.Model.DataTransferObject.OtherDto;
using College.Model.DataTransferObject.SalientFeaturesDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace College.Controllers
{
    [AuthOverride]
    public class WebSiteController : Controller
    {
        private readonly ILayoutRepo _repo;
        public WebSiteController(ILayoutRepo _repo)
        {
            this._repo = _repo;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region Salient Features

        public async Task<IActionResult> Features()
        {
            var dataSet = await _repo.FetchSalientFeaturesListAsyncTask();
            return View("Feature/Features", dataSet);
        }

        public IActionResult FeatureCreate()
        {
            var featureModel = new SalientFeaturesModelDto();
            return View("Feature/FeatureCreate", featureModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FeatureCreate(SalientFeaturesModelDto featuresModel)
        {
            if (ModelState.IsValid)
            {
                if (featuresModel != null)
                {
                    if (await _repo.CreateSalientFeaturesAsyncTask(featuresModel))
                    {
                        HttpContext.Session.SetString("Success", "Feature Created Successfully.");
                        return RedirectToAction(nameof(Features));
                    }
                    else
                    {
                        HttpContext.Session.SetString("Error", "Problem while adding the data!");
                        return RedirectToAction(nameof(Features));
                    }
                }
                else
                {
                    HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                    return RedirectToAction(nameof(Features));
                }
            }
            else
            {
                HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                return RedirectToAction(nameof(Features));
            }
        }

        public async Task<IActionResult> FeaturesUpdate(int id)
        {
            var dataSet = await _repo.FetchSalientFeaturesByIdAsyncTask(id);
            return View("Feature/FeaturesUpdate", dataSet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FeaturesUpdate(SalientFeaturesModelDto featuresModel)
        {
            if (ModelState.IsValid)
            {
                if (featuresModel != null)
                {
                    if (await _repo.UpdateSalientFeaturesAsyncTask(featuresModel))
                    {
                        HttpContext.Session.SetString("Success", "Feature Updated Successfully.");
                        return RedirectToAction(nameof(Features));
                    }
                    else
                    {
                        HttpContext.Session.SetString("Error", "Problem while updating the data!");
                        return RedirectToAction(nameof(Features));
                    }
                }
                else
                {
                    HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                    return RedirectToAction(nameof(Features));
                }
            }
            else
            {
                HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                return RedirectToAction(nameof(Features));
            }
        }

        [HttpPost]
        public async Task<IActionResult> FeaturesDelete(int id)
        {

            var dataSet = await _repo.FetchSalientFeaturesByIdAsyncTask(id);

            // Delete user
            if (await _repo.DeleteSalientFeaturesAsyncTask(dataSet.SalientFeatureId))
            {
                return Json("Success, Salient Feature deleted successfully");
            }
            else
            {
                return Json("Error Problem Deleting User");
            }
        }

        #endregion

        #region Footer

        public async Task<IActionResult> Footer()
        {
            var combinedModel = new FooterImportantLinkModelDto()
            {
                FooterUpdateModel = await _repo.FetchFooterHeaderAsyncTask(1),
                ImportantLinksModel = await _repo.FetchImportantLinksListAsyncTask()
            };
            return View("Footer/Footer", combinedModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateFooter(FooterUpdateModelDto footerUpdate)
        {
            if (ModelState.IsValid)
            {
                if (footerUpdate != null)
                {
                    if (await _repo.UpdateFooterHeaderAsyncTask(footerUpdate))
                    {
                        HttpContext.Session.SetString("Success", "Footer details Updated Successfully.");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        HttpContext.Session.SetString("Error", "Problem while updating the data!");
                        return RedirectToAction(nameof(Footer));
                    }
                }
                else
                {
                    HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                    return RedirectToAction(nameof(Footer));
                }
            }
            else
            {
                HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                return RedirectToAction(nameof(Footer));
            }
        }

        public IActionResult LinkItemCreate()
        {
            var model = new ImportantLinksModelDto();
            return View("Footer/LinkItemCreate", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LinkItemCreate(ImportantLinksModelDto linkModel)
        {
            if (ModelState.IsValid)
            {
                if (linkModel != null)
                {
                    if (await _repo.CreateImportantLinkAsyncTask(linkModel))
                    {
                        HttpContext.Session.SetString("Success", "Link Created Successfully.");
                        return RedirectToAction(nameof(Footer));
                    }
                    else
                    {
                        HttpContext.Session.SetString("Error", "Problem while adding the data!");
                        return RedirectToAction(nameof(Footer));
                    }
                }
                else
                {
                    HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                    return RedirectToAction(nameof(Footer));
                }
            }
            else
            {
                HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                return RedirectToAction(nameof(Footer));
            }
        }

        public async Task<IActionResult> LinkItemUpdate(int id)
        {
            var dataSet = await _repo.FetchImportantLinksByIdAsyncTask(id);
            return View("Footer/LinkItemUpdate", dataSet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LinkItemUpdate(ImportantLinksModelDto linkModel)
        {
            if (ModelState.IsValid)
            {
                if (linkModel != null)
                {
                    if (await _repo.UpdateImportantLinkAsyncTask(linkModel))
                    {
                        HttpContext.Session.SetString("Success", "Link Updated Successfully.");
                        return RedirectToAction(nameof(Footer));
                    }
                    else
                    {
                        HttpContext.Session.SetString("Error", "Problem while updating the data!");
                        return RedirectToAction(nameof(Footer));
                    }
                }
                else
                {
                    HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                    return RedirectToAction(nameof(Footer));
                }
            }
            else
            {
                HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                return RedirectToAction(nameof(Footer));
            }
        }

        [HttpPost]
        public async Task<IActionResult> LinkItemDelete(int id)
        {

            var dataSet = await _repo.FetchSalientFeaturesByIdAsyncTask(id);

            // Delete user
            if (await _repo.DeleteImportantLinkAsyncTask(dataSet.SalientFeatureId))
            {
                return Json("Success, Link deleted successfully");
            }
            else
            {
                return Json("Error Problem Deleting User");
            }
        }


        #endregion
    }
}
