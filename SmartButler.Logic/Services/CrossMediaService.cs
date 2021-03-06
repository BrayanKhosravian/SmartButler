﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using SmartButler.Logic.Interfaces;

#nullable enable
namespace SmartButler.Logic.Services
{
	public interface ICrossMediaService
	{
		Task<byte[]?> GetPhotoAsync();
	}

	public class CrossMediaService : ICrossMediaService
	{
		private readonly IUserInteraction _userInteraction;

		public CrossMediaService(IUserInteraction userInteraction)
		{
			_userInteraction = userInteraction;
		}

		public async Task<byte[]?> GetPhotoAsync()
		{
			var galleryOrCamera = await _userInteraction.DisplayActionSheetAsync("How to pick an image", 
				"cancel", null, "gallery", "camera", "delete image");

			MediaFile file = null;
			switch (galleryOrCamera)
			{
				case "gallery":
					file = await CrossMedia.Current.PickPhotoAsync();
					break;
				case "camera":
					file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions());
					break;
				case "delete image":
					return new byte[0];
				default:
					return null;
			}

			if (file == null) return null;

			using var stream = file.GetStream();
			var result = new byte[stream.Length];
			stream.Read(result, 0, (int) stream.Length);
			file.Dispose();

			return result;

		}

	}
}
#nullable restore