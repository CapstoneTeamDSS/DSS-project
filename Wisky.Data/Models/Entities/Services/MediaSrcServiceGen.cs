//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DSS.Data.Models.Entities.Services
{
    using System;
    using System.Collections.Generic;
    
    
    public partial interface IMediaSrcService : SkyWeb.DatVM.Data.IBaseService<MediaSrc>
    {
    }
    
    public partial class MediaSrcService : SkyWeb.DatVM.Data.BaseService<MediaSrc>, IMediaSrcService
    {
        public MediaSrcService(SkyWeb.DatVM.Data.IUnitOfWork unitOfWork, Repositories.IMediaSrcRepository repository) : base(unitOfWork, repository)
        {
        }
    }
}
