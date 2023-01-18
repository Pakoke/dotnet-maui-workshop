﻿using MonkeyFinder.Services;
using System.Data.SqlTypes;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MonkeyFinder.ViewModel;

public partial class MonkeysViewModel : BaseViewModel
{
	MonkeyService monkeyService;
	public ObservableCollection<Monkey> Monkeys { get; } = new ObservableCollection<Monkey>();

	public MonkeysViewModel(MonkeyService monkeyService)
	{
		Title = "Monkey Finder";
		this.monkeyService = monkeyService;	

	}

	[RelayCommand]
	async Task GetMonkeysAsync()
	{
		if(IsBusy) 
			return;
		try
		{
			IsBusy = true;
			var monkeys = await monkeyService.GetMonkeys();

			if (monkeys.Count != 0)
				Monkeys.Clear();
			
			foreach(var monkey in monkeys)
				Monkeys.Add(monkey);
		}
		catch (Exception ex)
		{
			Debug.WriteLine(ex);
			await Shell.Current.DisplayAlert("Error!", $"Unable to get monkeys: {ex.Message}", "OK");
		}
		finally
		{
			IsBusy = false;
		}
	}
}
