window.CentroEventosAuth = {
    setAuthUser: function (userId, userName, hours) {
        const expires = new Date(Date.now() + hours * 60 * 60 * 1_000).toUTCString();
        const data = `${userId}|${userName}`;
        const encodedData = btoa(data);
        document.cookie = `CentroEventosAuth=${encodedData}; expires=${expires}; path=/; SameSite=Lax; Secure`;
    },
    getAuthUser: function () {
        const name = 'CentroEventosAuth=';
        const decodedCookie = decodeURIComponent(document.cookie);
        const ca = decodedCookie.split(';');
        for (let i = 0; i < ca.length; i++) {
            let c = ca[i];
            while (c.charAt(0) === ' ') c = c.substring(1);
            if (c.indexOf(name) === 0) {
                const cookieValue = c.substring(name.length, c.length);
                const decodedValue = atob(cookieValue);
                const parts = decodedValue.split('|');
                if (parts.length === 2) {
                    return {
                        userId: +parts[0],
                        userName: parts[1]
                    };
                }
            }
        }
        return { userId: 0, userName: '' };
    },
    removeAuthUser: function () {
        document.cookie = 'CentroEventosAuth=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;';
    }
};
