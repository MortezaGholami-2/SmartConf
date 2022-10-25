using CommunityToolkit.WinUI.Notifications;

using SmartConf.WPF.Contracts.Services;

using Windows.UI.Notifications;

namespace SmartConf.WPF.Services;

public partial class ToastNotificationsService : IToastNotificationsService
{
    public ToastNotificationsService()
    {
    }

    public void ShowToastNotification(ToastNotification toastNotification)
    {
        ToastNotificationManagerCompat.CreateToastNotifier().Show(toastNotification);
    }
}
