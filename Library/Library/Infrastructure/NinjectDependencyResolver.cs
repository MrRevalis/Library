using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Domain.Abstract;
using Library.Domain.Concrete;
using Library.Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Identity;
using Microsoft.Owin.Security;
using Moq;
using Ninject;
using Ninject.Web.Common;

namespace Library.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            /*Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(x => x.Books).Returns(new List<Book>
            {
                new Book{Title = "Harry Potter", Author = "J.K. Rowling"},
                new Book{Title = "The Lord of the Rings", Author = "J.R.R. Tolkien"},
                new Book{Title = "Throne of Glass", Author = "Sarah J. Maas"}
            });

            kernel.Bind<IBookRepository>().ToConstant(mock.Object);*/
            kernel.Bind<IBookRepository>().To<EFBookRepository>();
            kernel.Bind<IShelfRepository>().To<EFShelfRepository>();
            //kernel.Bind<IAccountRepository>().To<EFAccountRepository>();
            kernel.Bind<AppUserManager>().ToMethod(x => HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>());
            kernel.Bind<IAuthenticationManager>().ToMethod(x => HttpContext.Current.GetOwinContext().Authentication).InRequestScope();

            kernel.Bind<AppRoleManager>().ToMethod(x => HttpContext.Current.GetOwinContext().GetUserManager<AppRoleManager>());
        }
    }
}