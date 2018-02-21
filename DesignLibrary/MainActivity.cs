using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using SupportFragment = Android.Support.V4.App.Fragment;
using SupportFragmentManager = Android.Support.V4.App.FragmentManager;
using SupportToolBar = Android.Support.V7.Widget.Toolbar;
using SupportActionBar = Android.Support.V7.App.ActionBar;
using Java.Lang;
using Android.Views;
using DesignLibrary.Fragments;

namespace DesignLibrary
{
    [Activity(Label = "DesignLibrary", MainLauncher = true, Theme = "@style/Theme.DesignDemo")]
    public class MainActivity : AppCompatActivity
    {
        private DrawerLayout mDrawerLayout;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //Support é só prefixo da biblioteca, essas merda n tem nada a ver com suporte
            //Esse aqui só ta capturando a toolbar do app
            SupportToolBar toolBar = FindViewById<SupportToolBar>(Resource.Id.toolBar);
            //Provavelmente tá definindo essa toolbar como uma/a ActionBar
            SetSupportActionBar(toolBar);

            //Não entendi pq instanciar outra se tinha tiha capturado uma 
            SupportActionBar ab = SupportActionBar;
            ab.SetHomeAsUpIndicator(Resource.Drawable.ic_menu); //Define o icone da ActionBar
            ab.SetDisplayHomeAsUpEnabled(true); //não sei pra que serve essa merda (talvez só habilita a action bar)

            
            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);

            if (navigationView != null)
            {
                SetUpDrawerContent(navigationView);
            }

            TabLayout tabs = FindViewById<TabLayout>(Resource.Id.tabs);

            //o ViewPager é o que possui o conteúdo dos Fragments
            ViewPager viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);
            SetUpViewPager(viewPager);
            
            //Definir as abas baseado no meu Viewpager
            tabs.SetupWithViewPager(viewPager);

            FloatingActionButton floatingActionButton = FindViewById<FloatingActionButton>(Resource.Id.fab);

            //o Is a sender >> sender Is a button >> button Is a View
            floatingActionButton.Click += (o, e) =>
            {
                var anchor = o as View;
                Snackbar.Make(anchor, "Yay Snackbar", Snackbar.LengthLong)
                        .SetAction("Action", v => 
                        {
                            //Do something here
                            //var intent = new Intent();
                        })
                        .Show();
            };
        }

        private void SetUpViewPager(ViewPager viewPager)
        {
            var adapter = new TabAdapter(SupportFragmentManager);
            adapter.AddFragment(new Fragment1(), "Fragment 1");
            adapter.AddFragment(new Fragment2(), "Fragment 2");
            adapter.AddFragment(new Fragment3(), "Fragment 3");
            

            //Comunicação com o TabAdapter
            viewPager.Adapter = adapter;
        }

        //Open the drawer when icon is clicked
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    mDrawerLayout.OpenDrawer((int)GravityFlags.Left);
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
            
        }

        //Create a listener that gets called when a user clicks on any item inside of the NavigationView
        private void SetUpDrawerContent(NavigationView navigationView)
        {
            //Turn everything selected in purple
            navigationView.NavigationItemSelected += (object sender, NavigationView.NavigationItemSelectedEventArgs e) =>
            {
                e.MenuItem.SetChecked(true);
                //Close drawer when item is clicked
                mDrawerLayout.CloseDrawers();
            };
        }

        #region TabAdapter
        public class TabAdapter : FragmentPagerAdapter
        {
            public List<SupportFragment> Fragments { get; set; }
            public List<string> FragmentNames { get; set; }

            public TabAdapter(SupportFragmentManager sFragmentManager) : base(sFragmentManager)
            {
                Fragments = new List<SupportFragment>();
                FragmentNames = new List<string>();
            }

            public void AddFragment(SupportFragment fragment, string name)
            {
                Fragments.Add(fragment);
                FragmentNames.Add(name);
            }

            public override int Count
            {
                get
                {
                    return Fragments.Count;
                }
            }

            public override SupportFragment GetItem(int position)
            {
                return Fragments[position];
            }

            public override ICharSequence GetPageTitleFormatted(int position)
            {
                return new String(FragmentNames[position]);
            }

        }
        #endregion

    }
}

