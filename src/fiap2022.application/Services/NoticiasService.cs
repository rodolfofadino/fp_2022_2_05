using fiap2022.application.Interfaces;
using fiap2022.domain.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fiap2022.application.Services
{
    public class NoticiaService : INoticiaService
    {
        private INoticiasReader _reader;
        private IMemoryCache _cache;

        public NoticiaService(IMemoryCache cache, INoticiasReader reader)
        {
            _reader = reader;
            _cache = cache;
        }

        public List<Noticia> Load(int totalDeNoticias, string categoria = "padrao")
        {
            var noticias = new List<Noticia>();
            var key = $"noticias_{categoria}";

            if (!_cache.TryGetValue(key, out noticias))
            {
                noticias = _reader.Load();

                var cacheEntryOption = new MemoryCacheEntryOptions()
                    //.SetSlidingExpiration()
                    .SetAbsoluteExpiration(DateTime.Now.AddSeconds(60));

                _cache.Set(key, noticias, cacheEntryOption);
            }

            return noticias.Where(a => a.Imagem != "").ToList();
        }

    }
}