using Android.OS;
using Android.Views;
using SupportFragment = Android.Support.V4.App.Fragment;


namespace DesignLibrary.Fragments
{
    public class Fragment3 : SupportFragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.Fragment2, container, false);

            return view;
        }
    }
}