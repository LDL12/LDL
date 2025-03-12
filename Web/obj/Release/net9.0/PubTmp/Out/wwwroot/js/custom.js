$.extend({
    custom: {
        post: function (handler, func, data)
        {
            $.ajax({
                url: '?handler=' + handler,
                type: 'POST',
                data: data,
                success: function (result)
                {
                    func(result);
                },
                error: function (xhr, status, error)
                {
                    console.log('Error: ' + error);
                },
                headers: {
                    RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val() // 添加防伪标记
                }
            });
        }
    }
});