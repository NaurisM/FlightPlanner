// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRegistry.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using AutoMapper;
using FlightPlanner.App_Start;
using FlightPlanner.Core.Data;
using FlightPlanner.Core.Services;
using FlightPlanner.Core.Services.Validators;
using StructureMap;

namespace FlightPlanner.DependencyResolution
{

    public class DefaultRegistry : Registry {
        #region Constructors and Destructors

        public DefaultRegistry() {
            Scan(
                scan => {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
					scan.With(new ControllerConvention());
                });
            var config = AutoMapperConfig.GetMapper();
            For<IMapper>().Use(config);
            For(typeof(IEntityService<>)).Use(typeof(EntityService<>));
            For<IFlightService>().Use<FlightService>();
            For<IAirportService>().Use<AirportService>();
            For<IFlightPlannerDbContext>().Use<FlightPlannerDbContext>();
            For<IFlightRequestValidator>().Use<AirportCodesValidator>();
            For<IFlightRequestValidator>().Use<AirportFromValidator>();
            For<IFlightRequestValidator>().Use<AirportToValidator>();
            For<IFlightRequestValidator>().Use<ArrivalDateValidator>();
            For<IFlightRequestValidator>().Use<CarrierValidator>();
            For<IFlightRequestValidator>().Use<DatesIntervalValidator>();
            For<IFlightRequestValidator>().Use<DepartureDateValidator>();
        }

        #endregion
    }
}