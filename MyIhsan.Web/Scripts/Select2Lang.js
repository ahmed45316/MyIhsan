var select2ArabicLang={
            errorLoading: function () {
                return 'حدث خطا اثناء جلب البيانات.';
            },
            inputTooLong: function (args) {
                var overChars = args.input.length - args.maximum;
                var message = 'اقصي عدد للبحث' + overChars + ' ';
                if (overChars >= 3 && overChars <= 10) {
                    message += 'حروف';
                } else {
                    message += 'حرف';
                }
                return message;
            },
            inputTooShort: function (args) {
                var remainingChars = args.minimum - args.input.length;

                var message = 'ادني عدد للبحث ' + remainingChars + ' ';
                if (remainingChars <= 2) {
                    message += 'حرف';
                } else {
                    message += 'حروف';
                }
                return message;
            },
            loadingMore: function () {
                return 'جاري التحميل...';
            },
            maximumSelected: function (args) {
                var message = 'لا تستطيع اختيار اكثر من ' + args.maximum + ' ';

                if (args.maximum <= 2 ) {
                    message += 'عنصر';
                } else {
                    message += 'عناصر';
                }

                return message;
            },
            noResults: function () {
                return 'لا توجد بيانات';
            },
            searching: function () {
                return 'جاري البحث...';
            }
}
var select2EnglishLang = {
    errorLoading: function () {
        return 'Error Loaing.';
    },
    inputTooLong: function (args) {
        var overChars = args.input.length - args.maximum;
        var message = 'Maximum character to search ' + overChars + ' ';
        if (overChars >= 3 && overChars <= 10) {
            message += 'character';
        } else {
            message += 'character';
        }
        return message;
    },
    inputTooShort: function (args) {
        var remainingChars = args.minimum - args.input.length;

        var message = 'Minimum character to search ' + remainingChars + ' ';
        if (remainingChars <= 2) {
            message += 'character';
        } else {
            message += 'character';
        }
        return message;
    },
    loadingMore: function () {
        return 'Loading...';
    },
    maximumSelected: function (args) {
        var message = 'Max Selected ' + args.maximum + ' ';

        if (args.maximum <= 2) {
            message += 'Item';
        } else {
            message += 'Items';
        }

        return message;
    },
    noResults: function () {
        return 'No Results';
    },
    searching: function () {
        return 'Searching...';
    }
}