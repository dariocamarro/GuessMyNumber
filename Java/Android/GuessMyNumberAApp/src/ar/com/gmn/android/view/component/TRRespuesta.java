package ar.com.gmn.android.view.component;

import android.content.Context;
import android.content.res.ColorStateList;
import android.graphics.Color;
import android.graphics.Typeface;
import android.widget.TableRow;
import android.widget.TextView;
import ar.com.gmn.android.core.Numero;
import ar.com.gmn.android.core.Respuesta;

public class TRRespuesta extends TableRow {
	private TextView turno;
	private NumeroView numero;
	private TextView bien;
	private TextView regular;

	public TRRespuesta(Context context, Respuesta r) {
		super(context);

		turno = new TextView(context);
		numero = new NumeroView(context,r.getNumero());
		bien = new TextView(context);
		bien.setText(r.getCorrectos().toString());
		regular = new TextView(context);
		regular.setText(r.getRegulares().toString());
		
		Typeface type = Typeface.createFromAsset(context.getAssets(),"fonts/EraserDust.ttf");
		turno.setTypeface(type);
		turno.setTextSize(30);
		turno.setTextColor(Color.WHITE);
		
		bien.setTypeface(type);
		bien.setTextSize(30);
		bien.setTextColor(Color.WHITE);
		
		regular.setTypeface(type);
		regular.setTextSize(30);
		regular.setTextColor(Color.WHITE);
		
		numero.setTypeface(type);
		numero.setTextSize(30);
		numero.setTextColor(Color.WHITE);
		
		this.addView(turno);
		this.addView(numero);
		this.addView(bien);
		this.addView(regular);
		
	}

	public void setBien(Integer i) {
		this.bien.setText(i.toString());
	}
	
	public void setRegular(Integer i) {
		this.regular.setText(i.toString());
	}
	
	public void setNumero(Numero n) {
		this.numero = new NumeroView(this.getContext(), n);
	}
	
	public void setTurno(Integer i) {
		this.turno.setText(i.toString());
	}

}
