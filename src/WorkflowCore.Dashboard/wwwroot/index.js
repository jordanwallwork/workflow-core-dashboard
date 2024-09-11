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

    },

    loadWorkflows: (id) => {
      Alpine.store('workflows', {
        init() {
          fetch(`workflows/${id}?page=${this.page}`)
            .then(resp => resp.json())
            .then(resp => this.instances = resp.items);
        },
        page: 1,
        instances: null
      })
    },

    loadWorkflowDetail: (defId, instanceId) => {
      Alpine.store('workflow', {
        init() {
          fetch(`workflows/${defId}/${instanceId}`)
            .then(resp => resp.json())
            .then(resp => this.detail = resp);
        },
        detail: null
      })
    },

    formatDateTime: (dateStr) => {
      const date = new Date(Date.parse(dateStr));
      return isNaN(date) ? "" : date.toLocaleString();
    }
  };
}
