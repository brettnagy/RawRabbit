﻿using System;
using RawRabbit.Logging;

namespace RawRabbit.vNext.Logging
{
	public class LoggingFactoryAdapter : ILoggerFactory
	{
		private readonly Microsoft.Extensions.Logging.ILoggerFactory _vNextFactory;

		public LoggingFactoryAdapter(Microsoft.Extensions.Logging.ILoggerFactory vNextFactory)
		{
			_vNextFactory = vNextFactory;
		}

		public void Dispose()
		{
			_vNextFactory.Dispose();
		}

		public LogLevel MinimumLevel
		{
			get
			{
				return (LogLevel)Enum.Parse(typeof(Microsoft.Extensions.Logging.LogLevel), _vNextFactory.MinimumLevel.ToString(), true);
			}
			set
			{
				_vNextFactory.MinimumLevel = (Microsoft.Extensions.Logging.LogLevel)Enum.Parse(typeof(LogLevel), value.ToString(), true);
			}
		}

		public ILogger CreateLogger(string categoryName)
		{
			var vNextLogger =  _vNextFactory.CreateLogger(categoryName);
			return new LoggerAdapter(vNextLogger);
		}
	}
}
