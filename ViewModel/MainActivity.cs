using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using System.Collections.Generic;
using Android.Arch.Lifecycle;
using Android.Support.V4.App;
using Android.Widget;

namespace ViewModel
{
    [Activity(Label = "ViewModel", MainLauncher = true, Icon = "@mipmap/ic_resource")]
	public class MainActivity : FragmentActivity
    {
		ItemViewModel ViewModel;
		RecyclerView RecyclerView;
        ProgressBar progress;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.Main);
			RecyclerView = FindViewById<RecyclerView>(Resource.Id.RecyclerView);
            progress = FindViewById<ProgressBar>(Resource.Id.progress);

			RecyclerView.SetLayoutManager(new LinearLayoutManager(this));

			ViewModel = ViewModelProviders.Of(this).Get(Java.Lang.Class.FromType(typeof(ItemViewModel))) as ItemViewModel;
            ViewModel.GetItems();
            ViewModel.OnItems += Response;
        }

        private void Response(object sender, List<Item> e)
        {
            RecyclerView.SetAdapter(new ItemAdapter { List = e });

            progress.Visibility = ViewStates.Gone;
        }

        protected override void OnStart()
		{
			base.OnStart();
		}

		class ItemAdapter : RecyclerView.Adapter
		{
			public List<Item> List;

			public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
			{
				var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ViewItem, parent, false);
				return new ItemViewHolder(view);
			}

			public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
			{
				var itemViewHolder = holder as ItemViewHolder;
				itemViewHolder.Name.Text = List[position].Name;
			}

			public override int ItemCount => List.Count;

			class ItemViewHolder : RecyclerView.ViewHolder
			{
				public TextView Name { get; set; }
				public ItemViewHolder(View itemView) : base(itemView)
				{
					Name = itemView.FindViewById<TextView>(Resource.Id.Name);
				}
			}
		}
	}
}

