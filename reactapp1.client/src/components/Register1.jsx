import React, { Component } from 'react'
import Form from 'react-validation/build/form'


const required = value => {
    if (!value) {
        return (

            <div className="alert alert-danger" role="alert">
                This field is required!
            </div>
        );
    }
};



export default class Register extends Component {
    constructor(props) {
        super(props);
        this.HandleRegister = this.HandleRegister.bind(this);
        this.onChangeEmail = this.onChangeEmail.bind(this);
        this.onChangePassword = this.onChangePassword.bind(this);

        this.state = { email: "", password:"", successful: false};
    }



    onChangeEmail(e) {
        this.setState({
            email: e.target.value
        });
    }
    onChangePassword() {
        this.setState({
            password: e.target.value
        });
    }

    render() {
        return (
            <div className="row">
                <div className="col">
                    <Form >
                        <div className="form-group">
                            <label htmlFor="email" >UserName</label>
                            <input type="email" className="form-control" name="email" value={this.state.email} onChange={this.onChangeEmail} />
                        </div>
                        <div className="form-group">
                            <label htmlFor="password" >Password</label>
                            <input type="password" className="form-control" name="password" value={this.state.password} onChange={this.onChangePassword} />
                        </div>

                        <div className="form-group">
                        <Button type="submit"> </Button>
                        </div>

                    </Form>

                </div>
            </div>
        );
    }
}