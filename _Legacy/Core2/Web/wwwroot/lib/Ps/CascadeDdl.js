//1. Create a class with the value Id, and Name
    //    public class GenericList 
    //    {
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    }
//2. Create your returning Class where you are going to get your data and convert it
    //public async Task<JsonResult> GetLabPosibleResults(int id)
    //{
    //    _db.Configuration.ProxyCreationEnabled = false;
    //    var dbList = _db.LaboratoryDetails.Where(m => m.LaboratoryId == id);
    ////or
    //    var dbList = await  _db.LaboratoryDetails.Where(m => m.LaboratoryId == id).ToListAsync;
    //    var rtList = new List<GenericList>();
    //    foreach(var item in dbList)
    //    {
    //        var element = new GenericList
    //        {
    //            Id = item.LaboratoryDetailId,
    //            Name = item.Result
    //        };
    //        rtList.Add(element);
    //    }
    //    return Json(rtList);
    //}
//3. Reference the JS on your view,
    //< script src = "~/Scripts/Ps/js/CascadeDdl.js" ></ script >
//4. If you want, fill with initial values your original list
    //var myFilledList = _db.Laboratories.Where(p => p.AuthorId == autorid).OrderBy(p => p.Name).ToList();
    //ViewBag.Products = myFilledList;

    //var myEmptyList = new List < Doctor >
    //{
    //    new Doctor { DoctorId = 0, Name = "-- Seleccione un Doctor" }
    //};
    //ViewBag.Doctors = myEmptyList;
//5. Receive on your view
    //var myFilledList = ViewBag.Products;
    //var myEmptyList = ViewBag.Doctors
//6. Add the class element to the master "MasterDdl" and to the details "DetailDdl"
    //   @Html.DropDownList("NameOfMaster", new SelectList(myFilledList, "Id", "Name"), "-- Seleccione una Prueba --", new { @class = "form-control MasterDdl" })
    //   @Html.DropDownList("NameOfDetail", new SelectList(myEmptyList, "Id", "Name"), "-- Seleccione un Resultado --", new { @class = "form-control DetailDdl" })
    //or if you are using razor
    //   @Html.DropDownList("Products", null, htmlAttributes: new { @class = "form-control MasterDdl" })
    //   @Html.DropDownList("Doctors", null, htmlAttributes: new { @class = "form-control DetailDdl" })
//7. Open a Script Section to declareted the Url, where it is gonna search the data (Point 2)
    //  <script type="text/javascript">
    //      var Urls = '@Url.Action("GetLabPosibleResults", "Laboratories")';
    //  </script >
//8. Enjoy it
    //Starling Germosen Reynoso www.fb.com/sgermosen24, www.fb.com/xamarindo, www.praysoft.net

$(document).ready(function () {
        $(".MasterDdl").change(function () {
            $(".DetailDdl").empty();
            $.ajax({
                type: "POST",
                url: Urls,
                dataType: "json",
                data: { id: $(".MasterDdl").val() },
                success: function (list) {
                    $.each(list,
                        function (i, item) {
                            $(".DetailDdl").append('<option value="' + item.Id + '">' + item.Name + "</option>");
                        });
                },
                error: function (ex) {
                    alert("Error al intentar traer los datos para llenar el detalle." + ex);
                }
            });
            return false;
        });
    });
 