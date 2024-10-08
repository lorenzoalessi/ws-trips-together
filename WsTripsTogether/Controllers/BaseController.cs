using Microsoft.AspNetCore.Mvc;
using WsTripsTogether.Dto.User;

namespace WsTripsTogether.Controllers;

public class BaseController : ControllerBase
{
    public UserLogged? UserLogged { get; set; }
}