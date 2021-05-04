using System.Web.Mvc;
using TrainingProject.Context;
using TrainingProject.Interfaces;
using TrainingProject.Services;
using TrainingProject.Util;
using Unity;
using Unity.Mvc5;

namespace TrainingProject
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            IUnityContainer container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            UnityContainer container = new UnityContainer();

            container.BindInRequestScope<IFileManager, FileManager>();
            container.BindInRequestScope<ApplicationDbContext, ApplicationDbContext>();
            return container;
        }
    }
}