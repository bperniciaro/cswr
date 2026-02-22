// <copyright file="TempDataViewComponent.cs" company="Fornits Web Solutions">
// Copyright (c) Fornits Web Solutions. All rights reserved.
// </copyright>

namespace Cswr.Web.ViewComponents
{
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Mvc;

	[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
	[ViewComponent]
	public class MessageBox : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync() => this.View("MessageBox");
	}
}