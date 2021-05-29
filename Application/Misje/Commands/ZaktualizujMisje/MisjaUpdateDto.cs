using AutoMapper;
using FocusOnFlying.Application.Common.Mappings;
using FocusOnFlying.Application.Extensions;
using FocusOnFlying.Application.Uslugi.Commands.UtworzUsluge;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FocusOnFlying.Application.Misje.Commands.ZaktualizujMisje
{
    public class MisjaUpdateDto : IMapFrom<Misja>, INotifyPropertyChanged
    {
        [OpenApiIgnore]
        public Guid Id { get; set; }
        #region Nazwa
        private string _nazwa;
        public string Nazwa
        {
            get => _nazwa;
            set
            {
                _nazwa = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Opis
        private string _opis;
        public string Opis
        {
            get => _opis;
            set
            {
                _opis = value;
                OnPropertyChanged();
            }
        }
        #endregion
        public Guid IdTypuMisji { get; set; }
        public int MaksymalnaWysokoscLotu { get; set; }
        public Guid IdStatusuMisji { get; set; }
        #region DataRozpoczecia
        private long? _dataRozpoczecia;
        public long? DataRozpoczecia
        {
            get => _dataRozpoczecia;
            set
            {
                _dataRozpoczecia = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region DataZakonczenia
        private long? _dataZakonczenia;
        public long? DataZakonczenia
        {
            get => _dataZakonczenia;
            set
            {
                _dataZakonczenia = value;
                OnPropertyChanged();
            }
        }
        #endregion
        public string IdPracownika { get; set; }
        #region SzerokoscGeograficzna
        private decimal _szerokoscGeograficzna;
        public decimal SzerokoscGeograficzna
        {
            get => _szerokoscGeograficzna;
            set
            {
                _szerokoscGeograficzna = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region DlugoscGeograficzna
        private decimal _dlugoscGeograficzna;
        public decimal DlugoscGeograficzna
        {
            get => _dlugoscGeograficzna;
            set
            {
                _dlugoscGeograficzna = value;
                OnPropertyChanged();
            }
        }
        #endregion
        public int Promien { get; set; }

        public List<MisjaDronDto> MisjeDrony { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Misja, MisjaUpdateDto>()
                .ForMember(dest => dest.DataRozpoczecia, opt => opt.MapFrom(src => src.DataRozpoczecia.ToUnixTime()))
                .ForMember(dest => dest.DataZakonczenia, opt => opt.MapFrom(src => src.DataZakonczenia.ToUnixTime()))
                .ReverseMap()
                .ForPath(dest => dest.DataRozpoczecia, opt => opt.MapFrom(src => src.DataRozpoczecia.ToLocalDateTime()))
                .ForPath(dest => dest.DataZakonczenia, opt => opt.MapFrom(src => src.DataZakonczenia.ToLocalDateTime()));
        }
    }
}
