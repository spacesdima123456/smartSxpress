namespace Wms.Updater
{
    public class UpdatingApp
    {
        public static void RunUpdate()
        {
            var info = InfoVersion.Instance();
            if (info.CurVersion < info.NewVersion)
            {
                var updater = new Update(info);
                updater.ShowDialog();
            }
        }
    }
}
