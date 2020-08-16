using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class UserLogic
    {
        public enum AuthType
        {
            Customer,
            Admin,
            TotallyNotRealAuthType
        }

        private EKMDemoEntities context = null;

        public UserLogic(EKMDemoEntities contextObj = null)
        {
            context = contextObj;
        }

        /// <summary>
        /// Given a username, password and auth level, method creates and returns a new SysUser database object
        /// </summary>
        /// <param name="authType">The auth type to set the user as, can be "Customer" or "Admin"</param>
        /// <returns>The created SysUser db entity</returns>
        /// <exception cref="RequestErrorException">Thrown when AuthType is not a valid selection</exception>
        public SysUser CreateNewUser(string user, string password, AuthType authType)
        {
            if (context == null)
                context = new EKMDemoEntities();

            var authAccess = context.Auths.FirstOrDefault(x => x.AuthType == authType.ToString());
            if (authAccess == null)
                throw new RequestErrorException("Auth type does not exist");

            var existingUser = context.SysUsers.FirstOrDefault(x => x.Username == user);
            if (existingUser != null)
                throw new RequestErrorException("Username already exists!");

            var userEntity = new SysUser
            {
                Username = user,
                Password = password,
                AuthID = authAccess.ID
            };
            context.SysUsers.Add(userEntity);
            context.SaveChanges();
            return userEntity;

        }

        /// <summary>
        /// Given a username and password, will attempt to match them to a record in the SysUser database
        /// </summary>
        /// <returns>Bool indicating whether login was successful or not</returns>
        public int Login(string userName, string password)
        {
            if (context == null)
                context = new EKMDemoEntities();

            var user = context.SysUsers.FirstOrDefault(x => x.Username == userName);
            if (user == null || user.Password != password)
                return 0;
            else
                return user.ID;

        }

        public SysUser GetUserByID(int id)
        {
            if (context == null)
                context = new EKMDemoEntities();

            var user = context.SysUsers.Where(x => x.ID == id).Include("Auth").FirstOrDefault();
            if (user == null)
                throw new RequestErrorException("User ID is not valid");
            else
                return user;

        }
    }
}
