function dashboard() {

  return {
    init: () => {

      Alpine.$router.config({ mode: 'hash', base: window.location.pathname });

      Alpine.store('definitions', {
        init() {
          fetch("definitions")
            .then(resp => resp.json())
            .then(resp => this.list = resp);
        },
        list: null
      });

    }
  }

}
