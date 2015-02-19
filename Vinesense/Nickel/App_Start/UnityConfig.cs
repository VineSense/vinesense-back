using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Nickel.Models;

namespace Nickel.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            container.RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager());

            container.RegisterType<IWeathersRepository, WeathersRepository>();
            container.RegisterType<IDbContextFactory, VinesenseDbContextFactory>();
            container.RegisterType<ISitesRepository, SitesRepository>();
            container.RegisterType<ILogsRepository, LogsRepositoy>();
            container.RegisterType<IGraphDataService, LinearInterExtrapolationGraphDataService>();
            container.RegisterType<IDepthOfInterestProvider, OneTwoThreeFourAndFiveDepthOfInterestProvider>();
            container.RegisterType<ISensorsRepository, SensorsRepository>();
            container.RegisterType<IServerInformationService, ServerInformationService>();
        }
    }
}
