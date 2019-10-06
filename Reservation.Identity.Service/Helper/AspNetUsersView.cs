
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyIhsan.Identity.Service.Extensions
{
   public static partial class ViewsQueries
    {
        public const string InsertAspNetUsers = "INSERT INTO aspnetusers (ID,EMAIL,PASSWORDHASH,PHONENUMBER,LOCKOUTENDDATEUTC,USERNAME,TELNUMBER,ADDRESS,GENDER,COUNTRYID,CITYID,NAME,ENTITYIDINFO,NAMEEN) VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13});";

        public const string UpdateAspNetUsers = "UPDATE aspnetusers SET EMAIL={0},PASSWORDHASH={1},PHONENUMBER={2},LOCKOUTENDDATEUTC={3},USERNAME={4},TELNUMBER={5},ADDRESS={6},GENDER={7},COUNTRYID={8},CITYID={9},NAME={10},ENTITYIDINFO={11},NAMEEN={12} WHERE ID={13}";

        public const string DeleteAspNetUsers = "UPDATE aspnetusers SET ISDELETED ={0} WHERE ID={1}";

        public const string GetAllAspNetUsers = "SELECT * FROM aspnetusers WHERE USERNAME like '%a%' ORDER BY ID OFFSET 10 ROWS FETCH NEXT 10 ROWS ONLY";

        public const string GetByIdAspNetUsers = "SELECT * FROM aspnetusers WHERE ID = {0}";

        public const string IsAspNetUsersExists = "SELECT * FROM aspnetusers WHERE(USERNAME = {0} or EMAIL = {1}) and ID<> {2}";
    }
}
