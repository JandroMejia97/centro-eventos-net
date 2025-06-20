window.CentroEventosAuth = {
    setAuthUser: function (userName, hours) {
        const expires = new Date(Date.now() + hours * 60 * 60 * 1_000).toUTCString();
        document.cookie = `CentroEventosAuth=${userName}; expires=${expires}; path=/`;
    },
    getAuthUser: function () {
        const name = 'CentroEventosAuth=';
        const decodedCookie = decodeURIComponent(document.cookie);
        const ca = decodedCookie.split(';');
        for (let i = 0; i < ca.length; i++) {
            let c = ca[i];
            while (c.charAt(0) === ' ') c = c.substring(1);
            if (c.indexOf(name) === 0) {
                return c.substring(name.length, c.length);
            }
        }
        return '';
    },
    removeAuthUser: function () {
        document.cookie = 'CentroEventosAuth=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;';
    }
};
