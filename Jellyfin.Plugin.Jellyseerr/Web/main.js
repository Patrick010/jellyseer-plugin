const pluginId = 'd921b930-cf91-406f-acbc-08914dcd0d34';

// Load configuration when the page loads
document.addEventListener('pageshow', () => {
    ApiClient.getPluginConfiguration(pluginId).then(config => {
        document.querySelector('#JellyseerrUrl').value = config.JellyseerrUrl || '';
        document.querySelector('#ApiKey').value = config.ApiKey || '';
        document.querySelector('#EnableRequestButton').checked = config.EnableRequestButton;
        document.querySelector('#ShowRequestStatus').checked = config.ShowRequestStatus;
        document.querySelector('#RateLimitDelay').value = config.RateLimitDelay || 1000;
    });
});

// Handle form submission
document.querySelector('.jellyseerrConfigForm').addEventListener('submit', e => {
    e.preventDefault();

    const config = {
        JellyseerrUrl: document.querySelector('#JellyseerrUrl').value,
        ApiKey: document.querySelector('#ApiKey').value,
        EnableRequestButton: document.querySelector('#EnableRequestButton').checked,
        ShowRequestStatus: document.querySelector('#ShowRequestStatus').checked,
        RateLimitDelay: parseInt(document.querySelector('#RateLimitDelay').value)
    };

    ApiClient.updatePluginConfiguration(pluginId, config).then(() => {
        Dashboard.processPluginConfigurationUpdateResult();
    });
}); 