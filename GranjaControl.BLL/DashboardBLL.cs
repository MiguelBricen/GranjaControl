using GranjaControl.DAL;
using GranjaControl.Entitys.Dto;

namespace GranjaControl.BLL
{
    public class DashboardBLL
    {
        private readonly DashboardDAL _dashboardDal;

        public DashboardBLL(DashboardDAL dashboardDal)
        {
            _dashboardDal = dashboardDal;
        }

        public async Task<DashboardDto> ObtenerAsync(int cantidadVentas = 5)
        {
            if (cantidadVentas < 1)
                cantidadVentas = 5;

            if (cantidadVentas > 50)
                cantidadVentas = 50;

            return await _dashboardDal.ObtenerAsync(cantidadVentas);
        }
    }
}
